using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entites;

namespace Store.Service.Products.Commands
{
    public class DeleteProductCommand
    {
        public int ProductId { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly StoreContext _context;

        public DeleteProductCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken = default)
        {
            var product = await GetProductAsync(request.ProductId, cancellationToken);

            if (product != null)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Product> GetProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.Products.SingleOrDefaultAsync(x => x.ProductId == productId, cancellationToken);
        }
    }
}
