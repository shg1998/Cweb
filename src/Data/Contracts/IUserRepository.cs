using Entities.User;

namespace Data.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        //Task<User> GetUserByIdAsync(int id , List<string> includeKeys,CancellationToken cancellationToken);
        Task AddAsync(User user, string password, CancellationToken cancellationToken);
        Task<User> GetByUserAndPass(string username, string password, CancellationToken cancellationToken);
        Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
        Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
    }
}
