using System.ComponentModel.DataAnnotations;

namespace Student_Hostel_Management_System.Data.Enums
{
    public enum enStatus
    {
        [Display(Name = " نشط أو نظامي")]
        Active = 1,
        [Display(Name = "غير نظامي")]
        Inactive = 2,
        [Display(Name = "موقف")]
        Suspended = 3,
        [Display(Name = "تخرج")]
        Graduated = 4,
        [Display(Name = "مفصول")]
        Expelled = 5
    }
}
