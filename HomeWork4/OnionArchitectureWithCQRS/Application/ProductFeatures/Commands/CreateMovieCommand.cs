using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.ProductFeatures.Commands
{
    public class CreateMovieCommand : IRequest<Movie>
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

    }

    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movie>
    {
        private readonly IApplicationDbContext _context;

        public CreateMovieCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
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

            return movie;
        }

    }

}
