using Common;
using Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Services.Dtos.Role;

namespace Services.Services.Role
{
    internal class RoleService : IRoleService, IScopedDependency
    {
        #region Private Fields
        private readonly RoleManager<Entities.Role.Role> _roleManager;
        #endregion

        #region ctor
        public RoleService(RoleManager<Entities.Role.Role> roleManager) => this._roleManager = roleManager;
        #endregion

        #region Public Methods
        public async Task<RoleDto> CreateRole(RoleDto roleDto, CancellationToken cancellationToken)
        {
            var res = await _roleManager.CreateAsync(new Entities.Role.Role
            {
                Name = roleDto.Name,
                Description = roleDto.Description
            });
            if (!res.Succeeded)
                throw new BadRequestException("There is a problem");
            return roleDto;
        }
        #endregion
    }
}
