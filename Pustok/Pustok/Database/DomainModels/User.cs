using Pustok.Database.Abstracts;

namespace Pustok.Database.DomainModels
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
