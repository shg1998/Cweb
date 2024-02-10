using Services.Dtos.Role;

namespace Services.Services.Role
{
    public interface IRoleService
    {
        Task<RoleDto> CreateRole(RoleDto roleDto, CancellationToken cancellationToken);
    }
}
