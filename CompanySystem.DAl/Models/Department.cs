using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanySystem.DAl.Models
{
    public class Department
    {
        public int id { get; set; } // By Convention => PK, Identity 

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        [InverseProperty("department")]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
