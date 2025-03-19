using CompanySystem.DAl.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanySystem.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int id { get; set; } // By Convention => PK, Identity 

        [Required(ErrorMessage = "Name Is Required..!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code Is Required..!")]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        [InverseProperty("department")]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
