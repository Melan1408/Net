using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
using Store.Data.Entites;
using System;

namespace Store.Service.Products.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<IList<ProductResponse>>
    {
        private readonly StoreContext _context;

        public GetProductsQueryHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<IList<ProductResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Select(x => new ProductResponse
                {
                    ProductId = x.ProductId,
                    CategoryId = x.CategoryId,
                    Name = x.Name,
                    Price = x.Price,
                    Category = new CategoryResponse
                    {
                        CategoryId = x.Category.CategoryId,
                        Name = x.Category.Name
                    }
                })
                .OrderByDescending(x => x.ProductId)
                .ToListAsync(cancellationToken);
        }
    }
}
