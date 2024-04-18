using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ProductFeatures.Queries
{
    public class GetSessionsQuery : IRequest<IEnumerable<Session>>
    {

    }
    public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, IEnumerable<Session>>
    {
        private readonly IApplicationDbContext _context;

        public GetSessionsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Session>> Handle(GetSessionsQuery query, CancellationToken cancellationToken)
        {
            var sessionList = await _context.Sessions.Include(x => x.Movie).ToListAsync(cancellationToken);

            return sessionList?.AsReadOnly();
        }
    }

}
