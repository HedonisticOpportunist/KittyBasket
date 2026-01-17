using CMSGTechnical.Domain.Interfaces;

namespace CMSGTechnical.Domain.Models
{
    public class MenuItem : IEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; } = 1;

        public string? Category { get; set; }

        public int Order { get; set; } = 0;

        public ICollection<MenuItem> ChildItems { get; set; } = new List<MenuItem>();
    }
}
