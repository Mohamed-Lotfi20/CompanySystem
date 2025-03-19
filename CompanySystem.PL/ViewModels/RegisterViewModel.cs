using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CompanySystem.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="First Name is required..!")]
		public string FName { get; set; }

		[Required(ErrorMessage = "Last Name is required..!")]
		public string LName { get; set; }

		[Required(ErrorMessage = "Email is required..!")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required..!")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is required..!")]
		[Compare("Password", ErrorMessage = "Confirm Password Does not Match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
	}
}
