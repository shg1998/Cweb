using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services.Dtos.User;
using Services.Services.User;
using Services.WebFramework.Pagination;
using WebFrameworks.Filters;

namespace CentralWeb.Controllers.User
{
    [Route("api/[controller]")]
    [ApiResultFilter]
    [ApiController]
    public class UsersController : ODataController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) => this._userService = userService;

        #region SuperAdmin
        [EnableQuery]
        [HttpGet("get-all-admins")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IQueryable<AdminDisplayDto>> GetAllAdmins(CancellationToken cancellationToken)
        {
            var queries = Request.QueryString.Value;
            var users = await this._userService.GetAllAdmins(queries, cancellationToken);
            Response.AddPaginationHeader(users.TotalPages, users.PageSize, users.CurrentPage, users.TotalCount);
            return users.Data;
        }

        [HttpPost("create-admin")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateAdmin(CreateUserDto userDto, CancellationToken cancellationToken)
        {
            await this._userService.CreateAdmin(userDto, cancellationToken);
            return Ok("Admin Created Successfully :)");
        }

        [HttpPut("edit-admin/{adminId:int}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> EditAdmin(int adminId, UserDto userDto, CancellationToken cancellationToken)
        {
            await this._userService.EditAdmin(adminId, userDto, cancellationToken);
            return Ok("Admin Edited Successfully :)");
        }

        [HttpDelete("delete-admin/{targetId:int}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteAdmin(int targetId, CancellationToken cancellationToken)
        {
            await this._userService.DeleteAdmin(targetId, cancellationToken);
            return Ok("User Deleted Successfully :)");
        }
        #endregion

        #region Admin
        [EnableQuery]
        [HttpGet("get-all-doctors")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IQueryable<UserDisplayDto>> GetAllDoctors(CancellationToken cancellationToken)
        {
            var queries = Request.QueryString.Value;
            var users = await this._userService.GetAllDoctors(queries, cancellationToken);
            Response.AddPaginationHeader(users.TotalPages, users.PageSize, users.CurrentPage, users.TotalCount);
            return users.Data;
        }

        [HttpPost("create-doctor")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> CreateDoctor(CreateUserDto userDto, CancellationToken cancellationToken)
        {
            await this._userService.CreateDoctor(userDto, cancellationToken);
            return Ok("Doctor Created Successfully :)");
        }

        [HttpPut("edit-doctor/{doctorId:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> EditDoctor(int doctorId, UserDto userDto, CancellationToken cancellationToken)
        {
            await this._userService.EditDoctor(doctorId, userDto, cancellationToken);
            return Ok("Doctor Edited Successfully :)");
        }

        [HttpDelete("delete-doctor/{targetId:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> DeleteDoctor(int targetId, CancellationToken cancellationToken)
        {
            var applicantId = int.Parse(User.Identity.GetUserId());
            await this._userService.DeleteDoctor(applicantId, targetId, cancellationToken);
            return Ok("Doctor Deleted Successfully :)");
        }
        #endregion

        #region shared between all roles
        [HttpPut("edit-user")]
        [Authorize(Roles = "SuperAdmin,Admin,Doctor")]
        public async Task<LoginResponseDto> EditByUser(EditUserDto userDto, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var result = await this._userService.EditUserByUser(userId, userDto, cancellationToken);
            return result;
        }
        #endregion

        #region AllowAnonymous
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<LoginResponseDto> Login(LoginDto loginDto, CancellationToken cancellationToken)
        {
            var token = await this._userService.Login(loginDto, cancellationToken);
            return token;
        }
        #endregion
    }
}
