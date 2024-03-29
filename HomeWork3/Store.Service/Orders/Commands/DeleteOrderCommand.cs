using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entites;

namespace Store.Service.Orders.Commands
{
    public class DeleteOrderCommand
    {
        public int OrderId { get; set; }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly StoreContext _context;

        public DeleteOrderCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderAsync(request.OrderId, cancellationToken);

            if (order != null)
            {
                _context.Remove(order);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.SingleOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
        }
    }
}
