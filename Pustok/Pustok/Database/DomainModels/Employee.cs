using Pustok.Database.Abstracts;

namespace Pustok.Database.DomainModels
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string Pin { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
