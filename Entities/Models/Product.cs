namespace Entities.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProdName { get; set; } = null!;

    public int Price { get; set; }

    public string ProdImage { get; set; } = null!;

    public int? CategoryId { get; set; }

    public string? ProdDescription { get; set; }

    public virtual Category? Category { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
