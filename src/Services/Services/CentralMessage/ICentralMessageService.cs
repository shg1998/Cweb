using Services.Dtos.Central;

namespace Services.Services.CentralMessage
{
    public interface ICentralMessageService
    {
        Task<List<string>> FindCentralMessageRelatedToSpecificUsers(int centralId, int bedsCount, CancellationToken cancellationToken);
        void GetDesiredCentral(int userId, int centralId);
        void ReleaseDesiredCentral(int userId, int centralId);
        int GetCentralActiveBeds(int centralId);
        Task<CentralMessageDto> GetAlarmsWithMessage(CentralMessageDto requestData);
    }
}
