using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProductFeatures.Queries
{
    public class GetMovieByIdQuery : IRequest<Movie>
    {
        public int MovieId { get; set; }
    }
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie>
    {
        private readonly IApplicationDbContext _context;

        public GetMovieByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Handle(GetMovieByIdQuery query, CancellationToken cancellationToken)
        {
            return await _context.Movies.Where(a => a.MovieId == query.MovieId).FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Movie not found!");
        }
    }

}
