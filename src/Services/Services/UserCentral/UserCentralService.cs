using AutoMapper;
using Common;
using Common.Exceptions;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Dtos.UserCentral;

namespace Services.Services.UserCentral
{
    internal class UserCentralService : IUserCentralService, IScopedDependency
    {
        #region Private Fields
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<Entities.User.User> _userManager;
        #endregion

        #region ctor
        public UserCentralService(ApplicationDbContext dbContext, IMapper mapper, UserManager<Entities.User.User> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region public methods

        //public async Task<PagedList<UserCentralMinimalDto>> GetAllUserCentral(UserCentralFilterDto filter, PaginationParameters paginationParams, CancellationToken cancellationToken)
        //{
        //    var query = _dbContext.Set<Entities.UserCentral.UserCentral>().AsNoTracking();
        //    query = this.FilterUserCentral(filter, query);

        //    var userCentrals = query.AsSingleQuery()
        //        .AsNoTrackingWithIdentityResolution()
        //        .ProjectTo<UserCentralMinimalDto>(_mapper.ConfigurationProvider)
        //        .AsQueryable();

        //    if (userCentrals == null)
        //        throw new BadRequestException("No Data !");

        //    var result = await PagedList<UserCentralMinimalDto>.ToPagedList(
        //        userCentrals, paginationParams.PageNumber, paginationParams.PageSize, cancellationToken);

        //    return result;
        //}

        //public async Task AddCentralAccessToUser(CreateUserCentralDto centralDto, CancellationToken cancellationToken)
        //{
        //    var userExist = await this._dbContext
        //                .Set<Entities.User.User>()
        //               .AnyAsync(u => u.Id == centralDto.UserId, cancellationToken);

        //    var centralExist = await this._dbContext
        //                .Set<Entities.Central.Central>()
        //               .AnyAsync(u => u.Id == centralDto.CentralId, cancellationToken);

        //    var isRepetitive = await this._dbContext
        //                 .Set<Entities.UserCentral.UserCentral>()
        //                .AnyAsync(u => u.UserId == centralDto.UserId && u.CentralId == centralDto.CentralId, cancellationToken);

        //    if (isRepetitive || !userExist || !centralExist)
        //        throw new BadRequestException("Access is Repetitive Or Some Problems in Sent Data ://");

        //    var userCentral = new Entities.UserCentral.UserCentral
        //    {
        //        UserId = centralDto.UserId,
        //        CentralId = centralDto.CentralId
        //    };

        //    await _dbContext.Set<Entities.UserCentral.UserCentral>().AddAsync(userCentral, cancellationToken);
        //    await _dbContext.SaveChangesAsync(cancellationToken);
        //}

        public async Task AddCentralAccessToUsers(CreateUsersCentralDto centralDto, CancellationToken cancellationToken)
        {
            var userCentrals = await _dbContext
                .Set<Entities.UserCentral.UserCentral>()
                .Where(b => b.CentralId == centralDto.CentralId).ToListAsync(cancellationToken);

            var admins = await _userManager.GetUsersInRoleAsync("Admin");

            foreach (var userCentral in userCentrals)
            {
                if (!admins.Contains(userCentral.User))
                    this._dbContext.Set<Entities.UserCentral.UserCentral>().Remove(userCentral);
                else
                    centralDto.UserIds.Remove(userCentral.UserId);
            }

            foreach (var userId in centralDto.UserIds)
            {
                try
                {
                    var userCentral = new Entities.UserCentral.UserCentral
                    {
                        UserId = userId,
                        CentralId = centralDto.CentralId
                    };
                    await _dbContext.Set<Entities.UserCentral.UserCentral>().AddAsync(userCentral, cancellationToken);
                }
                catch (BadRequestException)
                {
                }
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAllUserAccessibilities(int userId, CancellationToken cancellationToken)
        {
            var userCentrals = await _dbContext
                .Set<Entities.UserCentral.UserCentral>()
                .Where(b => b.UserId == userId).ToListAsync(cancellationToken);

            foreach (var userCentral in userCentrals)
                _dbContext.Set<Entities.UserCentral.UserCentral>().Remove(userCentral);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAllUserAccessibilitiesWithCentralId(List<Entities.UserCentral.UserCentral> centralUsers, CancellationToken cancellationToken)
        {
            foreach (var centralUser in centralUsers)
                _dbContext.Set<Entities.UserCentral.UserCentral>().Remove(centralUser);
        }

        //public async Task AddCentralsAccessToUser(CreateUserCentralsDto centralDto, CancellationToken cancellationToken)
        //{
        //    foreach (var centralId in centralDto.CentralIds)
        //    {
        //        try
        //        {
        //            var userCentral = new CreateUserCentralDto
        //            {
        //                UserId = centralDto.UserId,
        //                CentralId = centralId
        //            };
        //            await this.AddCentralAccessToUser(userCentral, cancellationToken);
        //        }
        //        catch (BadRequestException)
        //        { }
        //    }
        //}

        //public async Task DeleteAUserCentralAccessibility(int userCentralId, CancellationToken cancellationToken)
        //{
        //    var userCentralExist = await _dbContext
        //            .Set<Entities.UserCentral.UserCentral>()
        //            .SingleOrDefaultAsync(b => b.Id == userCentralId, cancellationToken);

        //    if (userCentralExist == null)
        //        throw new BadRequestException("There is a problem with the received data");

        //    _dbContext.Set<Entities.UserCentral.UserCentral>().Remove(userCentralExist);
        //    await _dbContext.SaveChangesAsync(cancellationToken);
        //}

        //public async Task DeleteUserCentralAccessibilities(DeleteUserCentralDto userCentralDto, CancellationToken cancellationToken)
        //{
        //    foreach (var userCentralId in userCentralDto.UserCentralIds)
        //    {
        //        try
        //        {
        //            await this.DeleteAUserCentralAccessibility(userCentralId, cancellationToken);
        //        }
        //        catch (BadRequestException)
        //        { }
        //    }
        //}

        #endregion
    }
}
