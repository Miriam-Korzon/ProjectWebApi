namespace DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int OrderSum { get; set; }

        public int UserId { get; set; }

        public IEnumerable<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
