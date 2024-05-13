using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Data.Context;
using MovieManagerReferenceData.Data.Entites;

namespace MovieManagerReferenceData.Service.Genres.Commands
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
    }

    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, bool>
    {
        private readonly MovieManagerReferenceDataContext _context;

        public DeleteGenreCommandHandler(MovieManagerReferenceDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteGenreCommand request, CancellationToken cancellationToken = default)
        {
            var genre = await GetGenreAsync(request.GenreId, cancellationToken);

            if (genre != null)
            {
                _context.Remove(genre);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Genre> GetGenreAsync(int genreId, CancellationToken cancellationToken = default)
        {
            return await _context.Genres.SingleOrDefaultAsync(x => x.GenreId == genreId, cancellationToken);
        }
    }
}
