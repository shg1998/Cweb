using System.ComponentModel.DataAnnotations;
using Services.Dtos.General;

namespace Services.Dtos.Central;

public class CreateCentralDto : BaseDtoComplexKey<CreateCentralDto, Entities.User.User>
{
    [Required]
    [StringLength(100)]
    public string UserName { get; set; }

    public string FullName { get; set; }

    [Required]
    [StringLength(500)]
    public string Password { get; set; }

    public bool IsActive { get; set; }

    public List<int> UserIds { get; set; }
}