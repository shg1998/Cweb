using Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Services.Dtos.Central;
using Services.Services.Central;
using Services.WebFramework.Pagination;
using WebFrameworks.Filters;

namespace CentralWebInHospital.Controllers.Central
{
    [Route("api/[controller]")]
    [ApiResultFilter]
    [ApiController]
    public class CentralController : ControllerBase
    {
        private readonly ICentralService _centralService;

        public CentralController(ICentralService centralService)
        {
            this._centralService = centralService;
        }

        [EnableQuery]
        [HttpGet("all")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IQueryable<CentralDisplayDto>> GetAllCentrals(CancellationToken cancellationToken)
        {
            var queries = Request.QueryString.Value;
            var centrals = await _centralService.GetAllCentrals(queries, cancellationToken);
            Response.AddPaginationHeader(centrals.TotalPages, centrals.PageSize, centrals.CurrentPage, centrals.TotalCount);
            return centrals.Data;
        }


        [HttpPost("create-central")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> CreateCentral(CreateCentralDto centralDto, CancellationToken cancellationToken)
        {
            await this._centralService.CreateCentral(centralDto, cancellationToken);
            return Ok("Central Created Successfully :)");
        }

        [HttpPut("edit-central/{centralId:int}")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> EditCentral(int centralId, EditCentralDto editedCentral, CancellationToken cancellationToken)
        {
            await this._centralService.EditCentral(centralId, editedCentral, cancellationToken);
            return Ok("Central Edited Successfully :)");
        }

        [HttpDelete]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<IActionResult> DeleteCentral(int centralId, CancellationToken cancellationToken)
        {
            await _centralService.DeleteACentral(centralId, cancellationToken);
            return Ok("Central Deleted Successfully :)");
        }

        [HttpGet("all-user-centrals")]
        [Authorize(Roles = "Doctor")]
        public async Task<List<CentralAbstractDto>> GetAllUserCentrals(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.Identity.GetUserId());
            var centrals = await _centralService.GetAllUserCentrals(userId, cancellationToken);
            return centrals;
        }
    }
}
