using AutoMapper;
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
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        public RoleController(RoleManager<IdentityRole> roleManager, IMapper _mapper)
        {
            this.roleManager = roleManager;
            mapper = _mapper;
        }


        
        #region Index
        public async Task<IActionResult> Index(string SearchValue)
        {// All Users
            ViewBag.SearchValue = SearchValue;

            if (string.IsNullOrEmpty(SearchValue))
            {
                var roles = await roleManager.Roles.Select(R => new RoleViewModel ()
                {
                    Id = R.Id,
                    RoleName = R.Name
                }).ToListAsync();
                return View(roles);
            }
            else
            {
                var role = await roleManager.FindByNameAsync(SearchValue);
                var MappedRole = new RoleViewModel()
                {
                    Id = role.Id,
                    RoleName = role.Name,

                };
                return View(new List<RoleViewModel>() { MappedRole });
            }
        }
        #endregion

        #region Create
        [HttpGet] // Get View
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleMV)
        {
            if (ModelState.IsValid)
            {
                var MapperRole = mapper.Map<RoleViewModel, IdentityRole>(roleMV);

                await roleManager.CreateAsync(MapperRole);
                return RedirectToAction(nameof(Index));
            }
            return View(roleMV);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
            var role = await roleManager.FindByIdAsync(id);

            if (User is null) return NotFound();
            var MapperRole = mapper.Map<IdentityRole,RoleViewModel>(role);
            return View(ViewName, MapperRole);
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
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleViewModel roleVM)
        {
            if (id != roleVM.Id) return BadRequest();


            if (ModelState.IsValid)
            {
                try
                {
                    var role = await roleManager.FindByIdAsync(id);
                    role.Name = roleVM.RoleName;
                    await roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. Log Exception
                    // 2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(roleVM);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(UserViewModel roleVM)
        {
            var role = await roleManager.FindByIdAsync(roleVM.id);
            await roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
