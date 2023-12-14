using Pustok.Database.Abstracts;

namespace Pustok.Database.DomainModels
{
    public class CatFact : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
