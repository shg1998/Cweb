using Services.Dtos.Central;
using Services.WebFramework.Pagination;

namespace Services.Services.Central
{
    public interface ICentralService
    {
        Task CreateCentral(CreateCentralDto centralDto, CancellationToken cancellationToken);
        Task DeleteACentral(int centralId, CancellationToken cancellationToken);
        Task<PagedQueryable<CentralDisplayDto>> GetAllCentrals(string? queries, CancellationToken cancellationToken);
        Task EditCentral(int centralId, EditCentralDto editedCentral, CancellationToken cancellationToken);
        Task<List<CentralAbstractDto>> GetAllUserCentrals(int userId, CancellationToken cancellationToken);
    }
}