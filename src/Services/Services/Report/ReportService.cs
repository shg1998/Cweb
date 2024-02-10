using Common;
using Data;
using Microsoft.AspNetCore.Identity;
using Services.Dtos.Report;

namespace Services.Services.Report
{
    internal class ReportService : IReportService, IScopedDependency
    {
        #region Private Fields
        private readonly UserManager<Entities.User.User> _userManager;
        #endregion

        #region ctor
        public ReportService(ApplicationDbContext dbContext, UserManager<Entities.User.User> userManager) => 
            _userManager = userManager;
        #endregion

        #region Public Methods
        public async Task<AdminReportDto> GetAdminReport(CancellationToken cancellationToken)
        {
            var centralTotalCount = await this._userManager.GetUsersInRoleAsync(RolesEnum.Central.ToString());
            var doctorTotalCount = await this._userManager.GetUsersInRoleAsync(RolesEnum.Doctor.ToString());
            return new AdminReportDto
            {
                CentralsCount = centralTotalCount.Count,
                DoctorsCount = doctorTotalCount.Count
            };
        }

        public async Task<SuperAdminReportDto> GetSuperAdminReport(CancellationToken cancellationToken)
        {
            var centralTotalCount = await this._userManager.GetUsersInRoleAsync(RolesEnum.Central.ToString());
            var doctorTotalCount = await this._userManager.GetUsersInRoleAsync(RolesEnum.Doctor.ToString());
            var adminsTotalCount = await this._userManager.GetUsersInRoleAsync(RolesEnum.Admin.ToString());
            return new SuperAdminReportDto
            {
                CentralsCount = centralTotalCount.Count,
                DoctorsCount = doctorTotalCount.Count,
                AdminsCount = adminsTotalCount.Count
            };
        }
        #endregion
    }
}
