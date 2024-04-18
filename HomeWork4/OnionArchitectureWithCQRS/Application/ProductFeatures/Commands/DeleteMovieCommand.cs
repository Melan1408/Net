using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProductFeatures.Commands
{
    public class DeleteMovieCommand : IRequest
    {
        public int MovieId { get; set; }
    }

    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteMovieCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteMovieCommand command, CancellationToken cancellationToken)
        {

            var movie = await _context.Movies.Where(a => a.MovieId == command.MovieId).FirstOrDefaultAsync(cancellationToken)
                ?? throw new Exception("Movie not found!");

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
