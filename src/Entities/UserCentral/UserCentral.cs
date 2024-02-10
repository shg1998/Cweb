using Entities.Common;

namespace Entities.UserCentral
{
    public class UserCentral : BaseEntity
    {
        public int UserId { get; set; }
        public int CentralId { get; set; }
        public User.User User { get; set; }
        public User.User Central { get; set; }
    }
}
