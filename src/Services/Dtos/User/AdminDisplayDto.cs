using Services.Dtos.General;

namespace Services.Dtos.User
{
    public class AdminDisplayDto : BaseDtoComplexKey<AdminDisplayDto, Entities.User.User>
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
    }
}