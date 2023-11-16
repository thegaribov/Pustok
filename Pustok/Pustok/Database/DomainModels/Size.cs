using Pustok.Database.Abstracts;

namespace Pustok.Database.DomainModels
{
    public class Size : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
