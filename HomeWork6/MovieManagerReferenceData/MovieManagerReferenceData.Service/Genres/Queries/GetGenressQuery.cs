using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Contract.Responses;
using MovieManagerReferenceData.Data.Context;

namespace MovieManagerReferenceData.Service.Genres.Queries
{
    public class GetGenressQueryHandler : IRequestHandler<IList<GenreResponse>>
    {
        private readonly MovieManagerReferenceDataContext _context;

        public GetGenressQueryHandler(MovieManagerReferenceDataContext context)
        {
            _context = context;
        }

        public async Task<IList<GenreResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Genres
                .AsNoTracking()
                .Select(x => new GenreResponse
                {
                    GenreId = x.GenreId,
                    Name = x.Name
                })
                .OrderByDescending(x => x.GenreId)
                .ToListAsync(cancellationToken);
        }
    }
}
