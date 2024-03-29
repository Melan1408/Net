using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
using Store.Data.Entites;
using System;

namespace Store.Service.Orders.Queries
{
    public class GetOrdersQueryHandler : IRequestHandler<IList<OrderResponse>>
    {
        private readonly StoreContext _context;

        public GetOrdersQueryHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<IList<OrderResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(x => x.Product)
                .ThenInclude(x => x.Category)
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
                .OrderByDescending(x => x.OrderId)
                .ToListAsync(cancellationToken);
        }
    }
}
