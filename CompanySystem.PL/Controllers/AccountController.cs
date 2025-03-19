using CompanySystem.DAl.Models;
using CompanySystem.PL.Helper;
using CompanySystem.PL.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace CompanySystem.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
		{
			this.userManager = _userManager;
			this.signInManager = _signInManager;
		}

		#region Register
		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid) // Server Side Validation
            {
                var User = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree
                };
                var Result = await userManager.CreateAsync(User, model.Password);
                if (Result.Succeeded) return RedirectToAction(nameof(Login));
                foreach (var error in Result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

			}
            return View(model);
        }
        #endregion

        #region Login
        [HttpGet]
		public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var flag = await userManager.CheckPasswordAsync(User, model.Password);
                    if (flag)
                    {
                        var result = await signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if (result.Succeeded) return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Password Is Invalid");
                        //ModelState.AddModelError(string.Empty, "Email Or Password Is Invalid");                    
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Is Invalid");
                    //ModelState.AddModelError(string.Empty, "Email Or Password Is Invalid");                    
                }
            }
			return View(model);
		}
		#endregion

		#region SignOut
		public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
		#endregion

		#region ForgetPassowrd
		public IActionResult ForgetPassowrd()
        {
			return View();
		}
        #endregion

        #region SendEmail
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var Token = await userManager.GeneratePasswordResetTokenAsync(User); // Token valid for one time
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = User.Email, token = Token }, Request.Scheme);
                    // https:\\localhost:44317/Account/ResetPassword/Mohamedlotfi3000@gmail.com&token=***********

                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = User.Email,
                        Body = ResetPasswordLink
					}; // Helper Fun => Static Fun 
                    EmailSetting.SendEmail(email); 
                    return RedirectToAction(nameof(CheckYourInBox));
                }
                ModelState.AddModelError(string.Empty, "Email is Invalid...!");
            }
            return View(model);
              //https:\\localhost:44317/Account/ResetPassword/Mohamedlotfi3000@gmail.com&token=asvberbfbserabe

        }
		#endregion

		#region CheckYourInBox
		public async Task<IActionResult> CheckYourInBox()
        {
            return View();
        }
        #endregion

        #region ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string email, string Token)
        {
            TempData["Email"] = email;
            TempData["Token"] = Token;
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
            if(ModelState.IsValid)
            {
                string email = TempData["Email"] as string;
                string token = TempData["Token"] as string;
				var User = await userManager.FindByEmailAsync(email);

                var Result = await userManager.ResetPasswordAsync(User, token, model.NewPassword);
                if (Result.Succeeded) return RedirectToAction(nameof(Login));
                foreach(var error in Result.Errors) ModelState.AddModelError(string.Empty, error.Description);
			}
            return View();
		}
		#endregion
	}
}
