using AutoMapper;
using CompanySystem.BLL.Interfaces;
using CompanySystem.BLL.Repositories;
using CompanySystem.DAl.Models;
using CompanySystem.PL.Helper;
using CompanySystem.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySystem.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly IMapper mapper;
        public UserController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager ,IMapper _mapper)
		{
			this.userManager = _userManager;
			this.signInManager = _signInManager;
			this.mapper = _mapper;
		}

        #region Index
        public async Task<IActionResult> Index(string SearchValue)
		{// All Users
			ViewBag.SearchValue = SearchValue;

			if (string.IsNullOrEmpty(SearchValue))
			{
				var Users = await userManager.Users.Select(U => new UserViewModel()
				{
					id = U.Id,
					FName = U.FName,
					LName = U.LName,
					Email = U.Email,
					PhoneNumber = U.PhoneNumber,
					Roles = userManager.GetRolesAsync(U).Result
				}).ToListAsync();
				return View(Users);
			}
			else
			{
				var User = await userManager.FindByEmailAsync(SearchValue);
				var MappedUser = new UserViewModel()
				{
					id = User.Id,
					FName = User.FName,
					LName = User.LName,
					Email = User.Email,
					PhoneNumber = User.PhoneNumber,
					Roles = userManager.GetRolesAsync(User).Result
				};
				return View(new List<UserViewModel>() { MappedUser});
			}
		}
        #endregion

        #region Details
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
            var User = await userManager.FindByIdAsync(id);

            if (User is null) return NotFound();
            var MapperEmp = mapper.Map<ApplicationUser, UserViewModel>(User);
            return View(ViewName, MapperEmp);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, UserViewModel userVM)
        {
            if (id != userVM.id) return BadRequest();

            
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    user.FName = userVM.FName;
                    user.LName = userVM.LName;
                    user.PhoneNumber = userVM.PhoneNumber;
                    user.SecurityStamp = Guid.NewGuid().ToString();
                    await userManager.UpdateAsync(user);                  
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. Log Exception
                    // 2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(userVM);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(UserViewModel UserVM)
        {
            var user = await userManager.FindByIdAsync(UserVM.id);
            await userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
