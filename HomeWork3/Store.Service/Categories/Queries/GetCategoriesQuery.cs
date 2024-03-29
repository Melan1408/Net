using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;

namespace Store.Service.Categories.Queries
{
    public class GetCategoriesQueryHandler : IRequestHandler<IList<CategoryResponse>>
    {
        private readonly StoreContext _context;

        public GetCategoriesQueryHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<IList<CategoryResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(x => new CategoryResponse
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name
                })
                .OrderByDescending(x => x.CategoryId)
                .ToListAsync(cancellationToken);
        }
    }
}
