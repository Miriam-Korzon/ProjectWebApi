using Entities.Models;


namespace Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Store325953446Context _store325953446Context;

        public OrderRepository(Store325953446Context store325953446Context)
        {
            _store325953446Context = store325953446Context;
        }

        public async Task<Order> AddOrder(Order order)
        {
            await _store325953446Context.Orders.AddAsync(order);
            await _store325953446Context.SaveChangesAsync();
            return order;
        }
    }
}
