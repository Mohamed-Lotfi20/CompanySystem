using System.Collections;
using System.Collections.Generic;

namespace CompanySystem.PL.ViewModels
{
	public class UserViewModel
	{
		public string id { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }

		public IEnumerable<string> Roles { get; set; }
	}
}
