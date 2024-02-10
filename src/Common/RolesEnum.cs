using System.ComponentModel.DataAnnotations;

namespace Common
{
    public enum RolesEnum
    {
        [Display(Name = "SuperAdmin")]
        SuperAdmin,
        [Display(Name = "Admin")]
        Admin,
        [Display(Name = "Doctor")]
        Doctor,
        [Display(Name = "Central")]
        Central
    }


}
