using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;

namespace Store.Service.Orders.Queries
{
    public class GetOrderByIdQueryHandler : IRequestHandler<int, OrderResponse>
    {
        private readonly StoreContext _context;

        public GetOrderByIdQueryHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> Handle(int orderId, CancellationToken cancellationToken = default)
        {

            return await _context.Orders
                .AsNoTracking()
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
                .Where(x => x.OrderId == orderId)
                .Select(x => new OrderResponse
                {
                    OrderId = x.OrderId,
                    Name = x.Name,
                    Product = new ProductResponse
                    {
                        ProductId = x.Product.ProductId,
                        CategoryId = x.Product.CategoryId,
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                        Category = new CategoryResponse
                        {
                            CategoryId = x.Product.Category.CategoryId,
                            Name = x.Product.Category.Name
                        }
                    }
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
