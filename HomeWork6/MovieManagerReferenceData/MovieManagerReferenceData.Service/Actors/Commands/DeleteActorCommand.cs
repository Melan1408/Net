using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Data.Context;
using MovieManagerReferenceData.Data.Entites;

namespace MovieManagerReferenceData.Service.Actors.Commands
{
    public class DeleteActorCommand
    {
        public int ActorId { get; set; }
    }

    public class DeleteActorCommandHandler : IRequestHandler<DeleteActorCommand, bool>
    {
        private readonly MovieManagerReferenceDataContext _context;

        public DeleteActorCommandHandler(MovieManagerReferenceDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteActorCommand request, CancellationToken cancellationToken = default)
        {
            var actor = await GetActorAsync(request.ActorId, cancellationToken);

            if (actor != null)
            {
                _context.Remove(actor);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }

        private async Task<Actor> GetActorAsync(int actorId, CancellationToken cancellationToken = default)
        {
            return await _context.Actors.SingleOrDefaultAsync(x => x.ActorId == actorId, cancellationToken);
        }
    }
}
