﻿using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProductFeatures.Commands
{
    public  class UpdateSessionCommand : IRequest
    {
        public int SessionId { get; set; }

        public int MovieId { get; set; }

        public string RoomName { get; set; }

        public DateTime StartDateTime { get; set; }
        public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateSessionCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task Handle(UpdateSessionCommand command, CancellationToken cancellationToken)
            {
                var session = await _context.Sessions.Where(a => a.SessionId == command.SessionId).FirstOrDefaultAsync(cancellationToken)
                    ?? throw new Exception("Session not found!");

                session.SessionId = command.SessionId;
                session.MovieId = command.MovieId;
                session.RoomName = command.RoomName;
                session.StartDateTime = command.StartDateTime;

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}