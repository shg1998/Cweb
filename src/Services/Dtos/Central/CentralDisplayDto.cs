using Services.Dtos.General;
using Services.Dtos.UserCentral;

namespace Services.Dtos.Central
{
    public class CentralDisplayDto : BaseDtoComplexKey<CentralDisplayDto, Entities.User.User>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserCentralDto> CentralUsers { get; set; }
    }
}