using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Report;
using Services.Services.Report;
using WebFrameworks.Filters;

namespace CentralWeb.Controllers.Report
{
    [Route("api/[controller]")]
    [ApiResultFilter]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService fileService) => _reportService = fileService;

        [HttpGet("get-admin-report")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<AdminReportDto> GetAdminReport(CancellationToken cancellationToken)
        {
            Trace.WriteLine("Comming :)))))))))))) Sooooooooooon");
            var report = await _reportService.GetAdminReport(cancellationToken);
            return report;
        }

        [HttpGet("get-s-admin-report")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<SuperAdminReportDto> GetSuperAdminReport(CancellationToken cancellationToken)
        {
            var report = await _reportService.GetSuperAdminReport(cancellationToken);
            return report;
        }
    }
}