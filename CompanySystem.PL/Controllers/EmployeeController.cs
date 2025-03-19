 using AutoMapper;
using CompanySystem.BLL.Interfaces;
using CompanySystem.BLL.Repositories;
using CompanySystem.DAl.Models;
using CompanySystem.PL.Helper;
using CompanySystem.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanySystem.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public EmployeeController(IUnitOfWork _unitOfWork, IMapper mapper)
        {
            this.unitOfWork = _unitOfWork;
            this.mapper = mapper;
        }
           
        #region Index
        public async Task<IActionResult> Index(string SearchValue)
        {
            ViewBag.SearchValue = SearchValue;
            IEnumerable<Employee> employeesVM;

            if (string.IsNullOrEmpty(SearchValue)) employeesVM = await unitOfWork.EmployeeRepository.GetAll();
            else employeesVM = unitOfWork.EmployeeRepository.GetEmployeeByName(SearchValue);

            var Employees = await unitOfWork.EmployeeRepository.GetAll();
            var MapperEmp = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employeesVM);
   
            return View(MapperEmp);
        }
        #endregion

        #region Create
        [HttpGet] // Get View
        public async Task<IActionResult> Create()
        {
            ViewBag.departments = await unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
                var MapperEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                if(employeeVM.Image is null) MapperEmp.ImageName = "";

                else MapperEmp.ImageName = DocumentSetting.UploadImage(employeeVM.Image,"Images");          
                await unitOfWork.EmployeeRepository.Add(MapperEmp);
                TempData["Message"] = $"{await unitOfWork.Complete()} Employees created ";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
            var employeeDetails = await unitOfWork.EmployeeRepository.GetByID(id);

            if (employeeDetails is null) return NotFound();
            var MapperEmp = mapper.Map<Employee, EmployeeViewModel>(employeeDetails);
            return View(ViewName, MapperEmp);
        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.departments = await unitOfWork.DepartmentRepository.GetAll();
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, EmployeeViewModel employeeVM)
        {
            var MapperEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            if (employeeVM.Image is null) MapperEmp.ImageName = "";
            else MapperEmp.ImageName = DocumentSetting.UploadImage(employeeVM.Image,"Images");
            if (id != employeeVM.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    
                    unitOfWork.EmployeeRepository.Update(MapperEmp);
                    TempData["Message"] = $"{await unitOfWork.Complete()} Employees Updated ";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1. Log Exception
                    // 2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(MapperEmp);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM)
        {
            var MapperEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            if(MapperEmp.ImageName is not null)
            {
                DocumentSetting.DeleteFile(MapperEmp.ImageName,"Images");
            }
            unitOfWork.EmployeeRepository.Delete(MapperEmp);
            TempData["Message"] =$"{await unitOfWork.Complete()} Employees Deleted ";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
