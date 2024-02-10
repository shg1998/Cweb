using Services.Dtos.General;
using Services.Dtos.User;

namespace Services.Dtos.UserCentral
{
    public class UserCentralDto : BaseDto<UserCentralDto, Entities.UserCentral.UserCentral>
    {
        public int UserId { get; set; }
        public int CentralId { get; set; }
        public UserDisplayDto User { get; set; }
    }
}
