using Services.Dtos.Report;

namespace Services.Services.Report
{
    public interface IReportService
    {
        Task<AdminReportDto> GetAdminReport(CancellationToken cancellationToken);

        Task<SuperAdminReportDto> GetSuperAdminReport(CancellationToken cancellationToken);
    }
}
