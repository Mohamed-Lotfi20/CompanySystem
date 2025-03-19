using CompanySystem.DAl.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanySystem.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Maximum Length is 50")]
        [MinLength(2, ErrorMessage = "Minimum Length is 2")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Address Is Required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Salary Is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [Range(22, 35, ErrorMessage = "Range Must be between (22,35)")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Is Required")]
        [Phone]
        public string Phone { get; set; }

        [Required(ErrorMessage = "IsActive Is Required")]
        public bool IsActive { get; set; }

        public IFormFile Image { get; set; }
        public string ImageName { get; set; } // ?For delete , update , ....

        [Required(ErrorMessage = "HireDate Is Required")]
        public DateTime HireDate { get; set; }

        [ForeignKey("department")]
        public int? DeptID { get; set; }

        [InverseProperty("Employees")]
        public Department department { get; set; }
    }
}
