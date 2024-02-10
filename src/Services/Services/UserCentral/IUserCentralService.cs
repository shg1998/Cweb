using Services.Dtos.UserCentral;

namespace Services.Services.UserCentral
{
    public interface IUserCentralService
    {
        //Task<PagedList<UserCentralMinimalDto>> GetAllUserCentral(UserCentralFilterDto filter, PaginationParameters paginationParams, CancellationToken cancellationToken);
        //Task AddCentralAccessToUser(CreateUserCentralDto centralDto, CancellationToken cancellationToken);
        Task AddCentralAccessToUsers(CreateUsersCentralDto centralDto, CancellationToken cancellationToken);
        Task DeleteAllUserAccessibilities(int userId, CancellationToken cancellationToken);
        Task DeleteAllUserAccessibilitiesWithCentralId(List<Entities.UserCentral.UserCentral> centralUsers, CancellationToken cancellationToken);
        //Task AddCentralsAccessToUser(CreateUserCentralsDto centralDto, CancellationToken cancellationToken);
        //Task DeleteAUserCentralAccessibility(int userCentralId, CancellationToken cancellationToken);
        //Task DeleteUserCentralAccessibilities(DeleteUserCentralDto userCentralDto, CancellationToken cancellationToken);
    }
}
