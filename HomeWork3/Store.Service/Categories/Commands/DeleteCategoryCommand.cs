using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entites;

namespace Store.Service.Categories.Commands
{
    public class DeleteCategoryCommand
    {
        public int CategoryId { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly StoreContext _context;

        public DeleteCategoryCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(request.CategoryId, cancellationToken);

            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Category> GetCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
        }
    }
}
