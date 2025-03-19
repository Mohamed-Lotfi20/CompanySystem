using System.ComponentModel.DataAnnotations;

namespace CompanySystem.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "New Password is required..!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is required..!")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password Does not Match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
