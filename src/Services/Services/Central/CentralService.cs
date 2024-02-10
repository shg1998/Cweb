using AutoMapper;
using Common;
using Common.Exceptions;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Dtos.Central;
using Services.Dtos.UserCentral;
using Services.Services.CentralMessage;
using Services.Services.Common;
using Services.Services.UserCentral;
using Services.WebFramework.Pagination;

namespace Services.Services.Central
{
    internal class CentralService : BaseService<CentralService>, ICentralService
    {
        #region Private Fields
        private readonly IUserCentralService _userCentralService;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Entities.User.User> _userManager;
        private readonly ICentralMessageService _centralMessageService;

        #endregion

        #region ctor
        public CentralService(IUserCentralService userCentralService, ILogger<CentralService> logger, ApplicationDbContext dbContext,
            IMapper mapper, UserManager<Entities.User.User> userManager, ICentralMessageService centralMessageService)
            : base(dbContext, logger, mapper)
        {
            this._userCentralService = userCentralService;
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._centralMessageService = centralMessageService;
        }
        #endregion

        #region Public Methods
        public async Task CreateCentral(CreateCentralDto centralDto, CancellationToken cancellationToken)
        {
            var isRepetitive = await this._dbContext
                .Set<Entities.User.User>()
                .AnyAsync(s => s.UserName == centralDto.UserName, cancellationToken);

            if (isRepetitive)
                throw new BadRequestException("Central is Repetitive ://");

            var central = base._mapper.Map<Entities.User.User>(centralDto);
            if (central == null)
                throw new BadRequestException("There is a problem with the received data");

            central.Email = central.UserName + "email" + new Random().Next(0, 999999999) + "@gmail.com";

            var passwordResult = await _userManager.CreateAsync(central, centralDto.Password);
            base.HandleIdentityManagerErrorResult(passwordResult);
            var roleResult = await _userManager.AddToRoleAsync(central, RolesEnum.Central.ToString());
            base.HandleIdentityManagerErrorResult(roleResult);
            await this._userCentralService.AddCentralAccessToUsers(new CreateUsersCentralDto
            {
                CentralId = central.Id,
                UserIds = centralDto.UserIds
            }, cancellationToken);
        }

        public async Task DeleteACentral(int centralId, CancellationToken cancellationToken)
        {
            var centralExist = await _dbContext
                .Set<Entities.User.User>().Include(s => s.CentralUsers)
                .SingleOrDefaultAsync(b => b.Id == centralId, cancellationToken);

            if (centralExist == null)
                throw new BadRequestException("There is a problem with the received data");

            await _userCentralService.DeleteAllUserAccessibilitiesWithCentralId(centralExist.CentralUsers.ToList(), cancellationToken);
            this._dbContext.Set<Entities.User.User>().Remove(centralExist);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedQueryable<CentralDisplayDto>> GetAllCentrals(string? queries, CancellationToken cancellationToken)
        {
            var centrals = await this.GetCentrals(cancellationToken);
            return PaginationHelper<Entities.User.User, CentralDisplayDto>.GeneratePagedQuery(base._mapper, centrals, queries);
        }

        public async Task EditCentral(int centralId, EditCentralDto editedCentral, CancellationToken cancellationToken)
        {
            var centralExist = await _dbContext
                .Set<Entities.User.User>()
                .SingleOrDefaultAsync(b => b.Id == centralId, cancellationToken);

            if (centralExist == null)
                throw new BadRequestException("There is a problem with the received data");
            await this.CheckForAdminAccessibility(centralExist);

            if (!editedCentral.Password.IsNullOrEmpty())
            {
                _ = await this._userManager.RemovePasswordAsync(centralExist);
                var resultAddPassword = await this._userManager.AddPasswordAsync(centralExist, editedCentral.Password);
                base.HandleIdentityManagerErrorResult(resultAddPassword);
            }
            centralExist.IsActive = editedCentral.IsActive;
            centralExist.FullName = editedCentral.FullName;
            await _userManager.SetUserNameAsync(centralExist, editedCentral.UserName);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await this._userCentralService.AddCentralAccessToUsers(new CreateUsersCentralDto
            {
                CentralId = centralExist.Id,
                UserIds = editedCentral.UserIds
            }, cancellationToken);
        }

        public async Task<List<CentralAbstractDto>> GetAllUserCentrals(int userId, CancellationToken cancellationToken)
        {
            var centrals = await this.GetCentrals(cancellationToken);
            var res = centrals.Where(s => s.IsActive == true && s.CentralUsers.Any(p => p.UserId == userId));
            var centralsDto = base._mapper.ProjectTo<CentralAbstractDto>(res).ToList();
            foreach (var centralDto in centralsDto)
                centralDto.ActiveBeds = this._centralMessageService.GetCentralActiveBeds(centralDto.Id);
            return centralsDto;
        }
        #endregion

        #region Private Methods
        private async Task CheckForAdminAccessibility(Entities.User.User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains(RolesEnum.Admin.ToString()))
                throw new UnauthorizedAccessException("You Can't Access To This Operation");
        }

        private async Task<IQueryable<Entities.User.User>> GetCentrals(CancellationToken cancellationToken)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == RolesEnum.Central.ToString(), cancellationToken);
            if (role == null)
                throw new NotFoundException("Not Found");

            var userIds = await _dbContext.UserRoles
                .Where(ur => ur.RoleId == role.Id)
                .Select(ur => ur.UserId)
                .ToListAsync(cancellationToken);

            //var query = await _userManager.GetUsersInRoleAsync(RolesEnum.Central.ToString());
            var centrals = _dbContext.Users
                .Where(u => userIds.Contains(u.Id))
                .Include(u => u.CentralUsers)
                .AsQueryable();

            return centrals;
        }
        #endregion
    }
}
