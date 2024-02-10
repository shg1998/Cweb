using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services.Dtos.History;
using Services.Services.Signal;
using Services.WebFramework.Pagination;
using WebFrameworks.Filters;

namespace CentralWebInHospital.Controllers.History
{
    [Route("api/[controller]")]
    [ApiResultFilter]
    [ApiController]
    public class HistoryController : ODataController
    {
        private readonly IHistoryService _historyService;

        public HistoryController(IHistoryService historyService) => _historyService = historyService;

        //[HttpGet("get-ecg-signal")]
        //[Authorize(Roles = "SuperAdmin,Admin,Doctor")]
        //public async Task<List<EcgSignalDto>> GetEcgSignal([FromQuery] GetEcgSignalDto getEcgSignalDto, CancellationToken cancellationToken)
        //{
        //    var result = await _historyService.GetEcgSignal(getEcgSignalDto, cancellationToken);
        //    return result;
        //}

        [HttpGet("get-history")]
        [Authorize(Roles = "SuperAdmin,Admin,Doctor")]
        public async Task<HistoryResponseDto> GetHistory([FromQuery] HistoryRequestDto historyRequestDto, CancellationToken cancellationToken)
        {
            var result = await this._historyService.GetHistory(historyRequestDto, cancellationToken);
            return result;
        }

        [EnableQuery]
        [HttpGet("get-alarm-history")]
        [Authorize(Roles = "SuperAdmin,Admin,Doctor")]
        public async Task<IQueryable<HistoryAlarmResponseDto>> GetAlarmHistory(CancellationToken cancellationToken)
        {
            var queries = Request.QueryString.Value;
            var alarms = await this._historyService.GetAlarmHistory(queries, cancellationToken);
            Response.AddPaginationHeader(alarms.TotalPages, alarms.PageSize, alarms.CurrentPage, alarms.TotalCount);
            return alarms.Data;
        }
    }
}