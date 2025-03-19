using AutoMapper;
using CompanySystem.BLL.Interfaces;
using CompanySystem.BLL.Repositories;
using CompanySystem.DAl.Models;
using CompanySystem.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanySystem.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public DepartmentController(IUnitOfWork _unitOfWork, IMapper mapper)
        {
            unitOfWork = _unitOfWork;
            this.mapper = mapper;
        }

        #region Index
        public async Task<IActionResult> Index(string SearchValue)
        {
            ViewBag.SearchValue = SearchValue;
            IEnumerable<Department> DepartmentsVM;
            
            if (string.IsNullOrEmpty(SearchValue)) DepartmentsVM = await unitOfWork.DepartmentRepository.GetAll();
            
            else DepartmentsVM = unitOfWork.DepartmentRepository.GetDepartmentByName(SearchValue);         
            
            var MapperDept = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(DepartmentsVM);
            return View(MapperDept);
        }
        #endregion

        #region Create
        [HttpGet] // Get View
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            var MapperDept = mapper.Map<DepartmentViewModel,Department>(departmentVM);
            if (ModelState.IsValid)
            {
                await unitOfWork.DepartmentRepository.Add(MapperDept);
                TempData["Message"] = $"{ await unitOfWork.Complete()} Departemt Created"; 
                return RedirectToAction(nameof(Index));
            }
            return View(MapperDept);     
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
            var DepartmentMV = await unitOfWork.DepartmentRepository.GetByID(id);
            if (DepartmentMV is null) return NotFound();
            var MapperDept = mapper.Map<Department, DepartmentViewModel>(DepartmentMV);
            return View(ViewName, MapperDept);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id,DepartmentViewModel departmentVM)
        {
            if(id != departmentVM.id) return BadRequest();
            var DeptMapper = mapper.Map<DepartmentViewModel, Department>(departmentVM);
            if (ModelState.IsValid)
            {
                try
                {
                   
                    unitOfWork.DepartmentRepository.Update(DeptMapper);
                    TempData["Message"] = $"{await unitOfWork.Complete()} Departemt Created";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. Log Exception
                    // 2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
            return View(DeptMapper);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM)
        {
            var DepMapper = mapper.Map<DepartmentViewModel, Department>(departmentVM);
            unitOfWork.EmployeeRepository.DeleteDeptIDFromEmployee(DepMapper.id);
            unitOfWork.DepartmentRepository.Delete(DepMapper);
            TempData["Message"] = $"{await unitOfWork.Complete()} Departemt Deleted";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
