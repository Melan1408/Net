using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;

namespace Store.Service.Products.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<int, ProductResponse>
    {
        private readonly StoreContext _context;

        public GetProductByIdQueryHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Where(x => x.CategoryId == productId)
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
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
