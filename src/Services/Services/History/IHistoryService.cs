using Services.Dtos.Central;
using Services.Dtos.History;
using Services.WebFramework.Pagination;

namespace Services.Services.Signal
{
    public interface IHistoryService
    {
        Task InsertReceivedData(int centralId, CentralMessageDto centralMessageDto, CancellationToken cancellationToken);
        //Task<List<EcgSignalDto>> GetEcgSignal(GetEcgSignalDto getEcgSignalDto, CancellationToken cancellationToken);
        Task<HistoryResponseDto> GetHistory(HistoryRequestDto historyRequestDto, CancellationToken cancellationToken);
        Task<PagedQueryable<HistoryAlarmResponseDto>> GetAlarmHistory(string? queries, CancellationToken cancellationToken);
    }
}
