using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
using Store.Data.Entites;
using System.ComponentModel.DataAnnotations;

namespace Store.Service.Products.Commands
{
    public class UpsertProductCommand
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public Product UpsertProduct()
        {
            var product = new Product
            {
                ProductId = ProductId,
                CategoryId = CategoryId,
                Name = Name,
                Price = Price,
            };

            return product;
        }
    }

    public class UpsertProductCommandHandler : IRequestHandler<UpsertProductCommand, ProductResponse>
    {
        private readonly StoreContext _context;

        public UpsertProductCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<ProductResponse> Handle(UpsertProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await GetProductAsync(request.ProductId, cancellationToken);

            if (product == null)
            {
                product = request.UpsertProduct();
                await _context.AddAsync(product, cancellationToken);
            }
            else
            {
                product.ProductId = request.ProductId;
                product.CategoryId = request.CategoryId;
                product.Name = request.Name;
                product.Price = request.Price;
            }
                   
            await _context.SaveChangesAsync(cancellationToken);

            return new ProductResponse
            {
                ProductId = product.ProductId,
                CategoryId = request.CategoryId,
                Name = request.Name,
                Price = request.Price,
                Category = product?.Category != null ? new CategoryResponse
                {
                    CategoryId = product.Category.CategoryId,
                    Name = product.Category.Name
                } : null
            };
        }

        private async Task<Product> GetProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Include(x => x.Category).SingleOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
        }
    }
}
