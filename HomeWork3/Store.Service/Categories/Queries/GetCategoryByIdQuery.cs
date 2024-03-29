using Microsoft.EntityFrameworkCore;
using Store.Contract.Responses;
using Store.Data.Context;

namespace Store.Service.Categories.Queries
{
    public class GetMovieByIdQueryHandler : IRequestHandler<int, CategoryResponse>
    {
        private readonly StoreContext _context;

        public GetMovieByIdQueryHandler(StoreContext context)
        {
            _context = context;
        }

        public async Task<CategoryResponse> Handle(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .Where(x => x.CategoryId == categoryId)
                .Select(x => new CategoryResponse
                {
                    CategoryId = x.CategoryId,
                    Name = x.Name
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
