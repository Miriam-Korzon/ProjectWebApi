namespace Entities.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public int OrderSum { get; set; }

    public int UserId { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? User { get; set; } = null!;
}
