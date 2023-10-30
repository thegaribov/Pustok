using Pustok.Database.DomainModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public int? Id { get; set; } //Id - ni nullable etdik cunki add eden zaman id movcud olmayacaq

        [NotMapped] //Add hissesinin melumat gonderende bura model binding olmasin
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string Pin { get; set; }
        public string Email { get; set; }

        public int DepartmentId { get; set; }
        public List<Department> Departments { get; set; }
    }
}
