using Services.Dtos.General;
using System.ComponentModel.DataAnnotations;

namespace Services.Dtos.Central
{
    public class EditCentralDto : BaseDtoComplexKey<EditCentralDto, Entities.User.User>
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(500)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public string FullName { get; set; }

        public List<int> UserIds { get; set; }
    }
}
