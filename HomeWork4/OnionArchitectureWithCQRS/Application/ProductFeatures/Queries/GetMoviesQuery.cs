using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProductFeatures.Queries
{
    public class GetMoviesQuery : IRequest<IEnumerable<Movie>>
    {
        public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<Movie>>
        {
            private readonly IApplicationDbContext _context;

            public GetMoviesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Movie>> Handle(GetMoviesQuery query, CancellationToken cancellationToken)
            {
                var movieList = await _context.Movies.ToListAsync(cancellationToken);

                return movieList?.AsReadOnly();
            }
        }
    }
}
