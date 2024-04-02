using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.ProductFeatures.Commands
{
    public class CreateSessionCommand : IRequest
    {
        public int SessionId { get; set; }

        public int MovieId { get; set; }

        public string RoomName { get; set; }

        public DateTime StartDateTime { get; set; }


        public class UpsertSessionCommandHandler : IRequestHandler<CreateSessionCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpsertSessionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(CreateSessionCommand command, CancellationToken cancellationToken)
            {
                var session = new Session
                {
                    SessionId = command.SessionId,
                    MovieId = command.MovieId,
                    RoomName = command.RoomName,
                    StartDateTime = command.StartDateTime
                };

                _context.Sessions.Add(session);
                await _context.SaveChangesAsync(cancellationToken);

            }
        }
    }
}
