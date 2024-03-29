using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;
using Store.Data.Entites;

namespace Store.Service.Categories.Commands
{
    public class UpsertCategoryCommand
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public Category UpsertCategory()
        {
            var category = new Category
            {
                CategoryId = CategoryId,
                Name = Name,
            };

            return category;
        }
    }

    public class UpsertCategoryCommandHandler : IRequestHandler<UpsertCategoryCommand, CategoryResponse>
    {
        private readonly StoreContext _context;

        public UpsertCategoryCommandHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse> Handle(UpsertCategoryCommand request, CancellationToken cancellationToken = default)
        {
            var category = await GetCategoryAsync(request.CategoryId, cancellationToken);

            if (category == null)
            {
                category = request.UpsertCategory();
                await _context.AddAsync(category, cancellationToken);
            }
            else
            {
                category.CategoryId = request.CategoryId;
                category.Name = request.Name;
            }
                   
            await _context.SaveChangesAsync(cancellationToken);

            return new CategoryResponse
            {
                CategoryId = category.CategoryId,
                Name = request.Name,
            };
        }

        private async Task<Category> GetCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.SingleOrDefaultAsync(x => x.CategoryId == categoryId, cancellationToken);
        }
    }
}
