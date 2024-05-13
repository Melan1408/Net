using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Data.Context;
using MovieManagerReferenceData.Data.Entites;

namespace MovieManagerReferenceData.Service.Directors.Commands
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }
    }

    public class DeleteDirectorCommandHandler : IRequestHandler<DeleteDirectorCommand, bool>
    {
        private readonly MovieManagerReferenceDataContext _context;

        public DeleteDirectorCommandHandler(MovieManagerReferenceDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken = default)
        {
            var director = await GetDirectorAsync(request.DirectorId, cancellationToken);

            if (director != null)
            {
                _context.Remove(director);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Director> GetDirectorAsync(int directorId, CancellationToken cancellationToken = default)
        {
            return await _context.Directors.SingleOrDefaultAsync(x => x.DirectorId == directorId, cancellationToken);
        }
    }
}
