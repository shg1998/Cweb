using Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace Entities.Role
{
    public class Role : IdentityRole<int>, IEntity
    {
        public string Description { get; set; }
    }
}
