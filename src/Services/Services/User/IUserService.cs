using Services.Dtos.User;
using Services.WebFramework.Pagination;

namespace Services.Services.User
{
    public interface IUserService
    {
        Task<LoginResponseDto> Login(LoginDto loginDto, CancellationToken cancellationToken);
        Task CreateDoctor(CreateUserDto userDto, CancellationToken cancellationToken);
        Task CreateAdmin(CreateUserDto userDto, CancellationToken cancellationToken);
        Task DeleteDoctor(int applicantId, int targetId, CancellationToken cancellationToken);
        Task DeleteAdmin(int targetId, CancellationToken cancellationToken);
        Task EditDoctor(int userId, UserDto userDto, CancellationToken cancellationToken);
        Task EditAdmin(int adminId, UserDto userDto, CancellationToken cancellationToken);
        Task<LoginResponseDto> EditUserByUser(int userId, EditUserDto userDto, CancellationToken cancellationToken);
        Task<PagedQueryable<AdminDisplayDto>> GetAllAdmins(string? queries, CancellationToken cancellationToken);
        Task<PagedQueryable<UserDisplayDto>> GetAllDoctors(string? queries, CancellationToken cancellationToken);
    }
}