using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Contract.Responses;
using MovieManagerReferenceData.Data.Context;
using MovieManagerReferenceData.Data.Entites;

namespace MovieManagerReferenceData.Service.Genres.Commands
{
    public class UpsertGenreCommand
    {
        public int GenreId { get; set; }

        public string Name { get; set; }

        public Genre UpsertGenre()
        {
            var genre = new Genre
            {
                GenreId = GenreId,
                Name = Name
            };

            return genre;
        }
    }

    public class UpsertGenreCommandHandler : IRequestHandler<UpsertGenreCommand, GenreResponse>
    {
        private readonly MovieManagerReferenceDataContext _context;

        public UpsertGenreCommandHandler(MovieManagerReferenceDataContext context)
        {
            _context = context;
        }

        public async Task<GenreResponse> Handle(UpsertGenreCommand request, CancellationToken cancellationToken = default)
        {
            var genre = await GetGenreAsync(request.GenreId, cancellationToken);

            if (genre == null)
            {
                genre = request.UpsertGenre();
                await _context.AddAsync(genre, cancellationToken);
            }
            else
            {
                genre.GenreId = request.GenreId;
                genre.Name = request.Name;
            }
                   
            await _context.SaveChangesAsync(cancellationToken);

            return new GenreResponse
            {
                GenreId = genre.GenreId,
                Name = request.Name
            };
        }

        private async Task<Genre> GetGenreAsync(int genreId, CancellationToken cancellationToken = default)
        {
            return await _context.Genres.SingleOrDefaultAsync(x => x.GenreId == genreId, cancellationToken);
        }
    }
}
