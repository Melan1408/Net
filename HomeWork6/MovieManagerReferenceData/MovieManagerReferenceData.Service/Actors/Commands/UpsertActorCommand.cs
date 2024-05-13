using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Contract.Responses;
using MovieManagerReferenceData.Data.Context;
using MovieManagerReferenceData.Data.Entites;

namespace MovieManagerReferenceData.Service.Actors.Commands
{
    public class UpsertActorCommand
    {
        public int ActorId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public Actor UpsertActor()
        {
            var actor = new Actor
            {
                ActorId = ActorId,
                Name = Name,
                Surname = Surname,
                Age = Age
            };

            return actor;
        }
    }

    public class UpsertActorCommandHandler : IRequestHandler<UpsertActorCommand, ActorResponse>
    {
        private readonly MovieManagerReferenceDataContext _context;

        public UpsertActorCommandHandler(MovieManagerReferenceDataContext context)
        {
            _context = context;
        }

        public async Task<ActorResponse> Handle(UpsertActorCommand request, CancellationToken cancellationToken = default)
        {
            var actor = await GetActorAsync(request.ActorId, cancellationToken);

            if (actor == null)
            {
                actor = request.UpsertActor();
                await _context.AddAsync(actor, cancellationToken);
            }
            else
            {
                actor.ActorId = request.ActorId;
                actor.Name = request.Name;
                actor.Surname = request.Surname;
                actor.Age = request.Age;
            }
                   
            await _context.SaveChangesAsync(cancellationToken);

            return new ActorResponse
            {
                ActorId = actor.ActorId,
                Name = request.Name,
                Surname = request.Surname,
                Age = request.Age
            };
        }

        private async Task<Actor> GetActorAsync(int actorId, CancellationToken cancellationToken = default)
        {
            return await _context.Actors.SingleOrDefaultAsync(x => x.ActorId == actorId, cancellationToken);
        }
    }
}
