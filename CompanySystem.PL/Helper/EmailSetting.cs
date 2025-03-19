using CompanySystem.DAl.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace CompanySystem.PL.Helper
{
	public class EmailSetting
	{
		public static void SendEmail(Email email)
		{

			var Clint = new SmtpClient("smtp.gmail.com", 587);
			Clint.EnableSsl = true;
			Clint.Credentials = new NetworkCredential("mohamedlotfi3000@gmail.com", "sijzsenswalpieue"); // Who Send Email ?
			Clint.Send("mohamedlotfi3000@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
