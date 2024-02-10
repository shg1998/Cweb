using AutoMapper;
using Common;
using Common.Exceptions;
using Data;
using Data.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.Dtos.User;
using Services.Services.Auth;
using Services.Services.Common;
using Services.WebFramework.Pagination;

namespace Services.Services.User
{
    internal class UserService : BaseService<UserService>, IUserService
    {
        #region Private Fields
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly UserManager<Entities.User.User> _userManager;
        #endregion

        #region ctor
        public UserService(ApplicationDbContext dbContext,
            IMapper mapper, IUserRepository userRepository, ILogger<UserService> logger, IJwtService jwtService,
            UserManager<Entities.User.User> userManager) : base(dbContext, logger, mapper)
        {
            this._userRepository = userRepository;
            this._jwtService = jwtService;
            this._userManager = userManager;
        }
        #endregion

        #region Public Methods
        public async Task CreateDoctor(CreateUserDto userDto, CancellationToken cancellationToken) =>
            await this.AddUserWithCustomRole(userDto, RolesEnum.Doctor, cancellationToken);

        public async Task CreateAdmin(CreateUserDto userDto, CancellationToken cancellationToken) =>
            await this.AddUserWithCustomRole(userDto, RolesEnum.Admin, cancellationToken);

        public async Task EditDoctor(int doctorId, UserDto userDto, CancellationToken cancellationToken)
        {
            var userExists = await _dbContext
                .Set<Entities.User.User>()
                .SingleOrDefaultAsync(b => b.Id == doctorId, cancellationToken);
            if (userExists == null)
                throw new BadRequestException("There is a problem with the received data");
            if (!await _userManager.IsInRoleAsync(userExists, RolesEnum.Doctor.ToString()))
                throw new UnauthorizedAccessException("User is not doctor");
            await this.ApplyEditChanges(userExists, userDto, cancellationToken);
        }

        public async Task EditAdmin(int adminId, UserDto userDto, CancellationToken cancellationToken)
        {
            var userExists = await _dbContext
                .Set<Entities.User.User>()
                .SingleOrDefaultAsync(b => b.Id == adminId, cancellationToken);

            if (userExists == null)
                throw new BadRequestException("There is a problem with the received data");

            await this.ApplyEditChanges(userExists, userDto, cancellationToken);
        }

        public async Task<LoginResponseDto> EditUserByUser(int userId, EditUserDto userDto, CancellationToken cancellationToken)
        {
            var userExists = await _dbContext
                .Set<Entities.User.User>()
                .SingleOrDefaultAsync(b => b.Id == userId, cancellationToken);
            if (userExists == null)
                throw new BadRequestException("There is a problem with the received data");
            var areNotPasswordFieldsNullOrEmpty = !string.IsNullOrEmpty(userDto.NewPassword) &&
                                                  !string.IsNullOrEmpty(userDto.OldPassword);
            if (areNotPasswordFieldsNullOrEmpty)
            {
                var isOldPasswordCorrect = await this._userManager.CheckPasswordAsync(userExists, userDto.OldPassword);
                var areNotOldPasswordAndNewPasswordEqual = userDto.OldPassword != userDto.NewPassword;
                if (isOldPasswordCorrect && areNotOldPasswordAndNewPasswordEqual)
                {
                    var resultChangePassword =
                        await this._userManager.ChangePasswordAsync(userExists, userDto.OldPassword, userDto.NewPassword);
                    base.HandleIdentityManagerErrorResult(resultChangePassword);
                }
                else
                    throw new BadRequestException("There is a problem with the received data");
            }
            await _userManager.SetUserNameAsync(userExists, userDto.UserName);
            await _userManager.SetEmailAsync(userExists, userDto.Email);
            userExists.FullName = userDto.FullName;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return GenerateUserResponseLoginDto(userExists).Result;
        }

        public async Task DeleteDoctor(int applicantId, int userId, CancellationToken cancellationToken)
        {
            base.CheckForInvalidEqualId(applicantId, userId);
            var user = await _dbContext.Set<Entities.User.User>()
                .FirstOrDefaultAsync(s => s.Id == userId, cancellationToken);
            if (user == null)
                throw new NotFoundException("There is Problem !");
            if (!await _userManager.IsInRoleAsync(user, RolesEnum.Doctor.ToString()))
                throw new UnauthorizedAccessException("User is not doctor");
            await _userRepository.DeleteAsync(user, cancellationToken);
        }

        public async Task DeleteAdmin(int userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
            if (user == null)
                throw new NotFoundException("There is Problem !");
            await _userRepository.DeleteAsync(user, cancellationToken);
        }

        public async Task<PagedQueryable<UserDisplayDto>> GetAllDoctors(string? queries, CancellationToken cancellationToken)
        {
            var users = await this.GetUsersInRoleWithIncludeAsync(RolesEnum.Doctor, "UserCentrals");
            return PaginationHelper<Entities.User.User, UserDisplayDto>.GeneratePagedQuery(base._mapper, users, queries);
        }

        public async Task<PagedQueryable<AdminDisplayDto>> GetAllAdmins(string? queries, CancellationToken cancellationToken)
        {
            var query = await _userManager.GetUsersInRoleAsync(RolesEnum.Admin.ToString());
            return PaginationHelper<Entities.User.User, AdminDisplayDto>.GeneratePagedQuery(base._mapper, query.AsQueryable(), queries);
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
                throw new BadRequestException("Username Or Password is Invalid");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
                throw new BadRequestException("Username Or Password is Invalid");

            if (!user.IsActive)
                throw new BadRequestException("Your account is waiting for admin approval");

            return GenerateUserResponseLoginDto(user).Result;
        }
        #endregion

        #region Private Methods
        private async Task ApplyEditChanges(Entities.User.User userExists, UserDto userDto, CancellationToken cancellationToken)
        {
            if (!userDto.Password.IsNullOrEmpty())
            {
                await this._userManager.RemovePasswordAsync(userExists);
                var resultAddPassword = await this._userManager.AddPasswordAsync(userExists, userDto.Password);
                base.HandleIdentityManagerErrorResult(resultAddPassword);
            }
            userExists.FullName = userDto.FullName;
            userExists.IsActive = userDto.IsActive;
            await _userManager.SetUserNameAsync(userExists, userDto.UserName);
            await _userManager.SetEmailAsync(userExists, userDto.Email);
            await _dbContext.SaveChangesAsync(cancellationToken);
            //if (userDto.Image != null)
            //    await this.UploadUserImage(userExists, userDto.Image, cancellationToken);
        }

        private async Task AddUserWithCustomRole(CreateUserDto userDto, RolesEnum role, CancellationToken cancellationToken)
        {
            var userExists = await _dbContext
                .Set<Entities.User.User>()
                .SingleOrDefaultAsync(p => p.UserName == userDto.UserName || p.Email == userDto.Email, cancellationToken);
            if (userExists != null)
                throw new BadRequestException("Username or Email is Duplicate !");
            var user = base._mapper.Map<Entities.User.User>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            base.HandleIdentityManagerErrorResult(result);
            var roleResult = await _userManager.AddToRoleAsync(user, role.ToString());
            base.HandleIdentityManagerErrorResult(roleResult);
        }

        private async Task<LoginResponseDto> GenerateUserResponseLoginDto(Entities.User.User user)
        {
            var userResponseLoginDto = new LoginResponseDto
            {
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
                Token = await _jwtService.GenerateAsync(user),
                UserRoleName = _userManager.GetRolesAsync(user).Result.First()
            };
            return userResponseLoginDto;
        }

        private async Task<IQueryable<Entities.User.User>> GetUsersInRoleWithIncludeAsync(RolesEnum roleName, string includeProperties)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName.ToString());
            var usersIds = users.Select(u => u.Id).ToList();
            return _dbContext.Users.Where(u => usersIds.Contains(u.Id)).Include(includeProperties);
        }
        #endregion
    }
}
