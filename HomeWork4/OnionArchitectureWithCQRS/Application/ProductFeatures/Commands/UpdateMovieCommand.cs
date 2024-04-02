using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProductFeatures.Commands
{
    public  class UpdateMovieCommand : IRequest
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateMovieCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
            {
                var movie = await _context.Movies.Where(a => a.MovieId == command.MovieId).FirstOrDefaultAsync(cancellationToken) 
                    ?? throw new Exception("Movie not found!");

                movie.MovieId = command.MovieId;
                movie.Title = command.Title;
                movie.Description = command.Description;
                movie.ReleaseDate = command.ReleaseDate;

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
