using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.ProductFeatures.Commands
{
    public class CreateMovieCommand : IRequest
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public class UpsertMovieCommandHandler : IRequestHandler<CreateMovieCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpsertMovieCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(CreateMovieCommand command, CancellationToken cancellationToken)
            {
                var movie = new Movie
                {
                    MovieId = command.MovieId,
                    Title = command.Title,
                    Description = command.Description,
                    ReleaseDate = command.ReleaseDate
                };

                _context.Movies.Add(movie);
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
