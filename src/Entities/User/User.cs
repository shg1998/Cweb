using Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace Entities.User
{
    public class User : IdentityUser<int>,IEntity
    {
        public string? FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public ICollection<UserCentral.UserCentral> UserCentrals { get; set; }
        public ICollection<UserCentral.UserCentral> CentralUsers { get; set; }
    }
}
