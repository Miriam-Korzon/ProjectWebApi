using Entities.Models;
using Microsoft.Extensions.Logging;
using Repositories;


namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Order> AddOrder(Order order)
        { 
            int sum = CalculatePrice(order.OrderItems).Result;
            if(order.OrderSum != sum)
            {
                _logger.LogError($"user {order.UserId} tried to steal");
                order.OrderSum = sum;
            }
            return await _orderRepository.AddOrder(order);
        }

        private async Task<int> CalculatePrice(IEnumerable<OrderItem> orderItems)
        {
            int price = 0;
            List<Product> products = await _productRepository.GetAllProducts();
            foreach (OrderItem orderItem in orderItems) {
                Product prod = products.Find(p => p.Id == orderItem.ProductId);
                if (prod != null)
                {
                    price += prod.Price * orderItem.Quantity;
                }
            }
            return price;
        }
    }


}
