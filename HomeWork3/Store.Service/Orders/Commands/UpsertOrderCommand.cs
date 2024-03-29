using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
using Store.Data.Entites;
using System.ComponentModel.DataAnnotations;

namespace Store.Service.Orders.Commands
{
    public class UpsertOrderCommand
    {
        public int OrderId { get; set; }

        public string Name { get; set; }

        public int ProductId { get; set; }

        public Order UpsertOrder()
        {
            var order = new Order
            {
                OrderId = OrderId,
                Name = Name,
                ProductId = ProductId
            };
            return order;
        }
    }

    public class UpsertOrderCommandHandler : IRequestHandler<UpsertOrderCommand, OrderResponse>
    {
        private readonly StoreContext _context;

        public UpsertOrderCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(UpsertOrderCommand request, CancellationToken cancellationToken = default)
        {
            var order = await GetOrderAsync(request.OrderId, cancellationToken);

            if (order == null)
            {
                order = request.UpsertOrder();
                await _context.AddAsync(order, cancellationToken);
            }
            else
            {
                order.OrderId = request.OrderId;
                order.Name = request.Name;
                order.ProductId = request.ProductId;
            }
                   
            await _context.SaveChangesAsync(cancellationToken);

            return new OrderResponse
            {
                OrderId = order.OrderId,
                Name = order.Name,
                Product = order?.Product != null ? new ProductResponse
                {
                    ProductId = order.Product.ProductId,
                    CategoryId = order.Product.CategoryId,
                    Name = order.Product.Name,
                    Category = order?.Product.Category != null ? new CategoryResponse
                    {
                        CategoryId = order.Product.Category.CategoryId,
                        Name = order.Product.Category.Name
                    } : null
                } : null
            };
        }

        private async Task<Order> GetOrderAsync(int orderId, CancellationToken cancellationToken = default)
        {
            return await _context.Orders.Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .SingleOrDefaultAsync(x => x.OrderId == orderId, cancellationToken);
        }
    }
}
