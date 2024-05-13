using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Contract.Responses;
using MovieManagerReferenceData.Data.Context;

namespace MovieManagerReferenceData.Service.Actors.Queries
{
    public class GetActorsQueryHandler : IRequestHandler<IList<ActorResponse>>
    {
        private readonly MovieManagerReferenceDataContext _context;

        public GetActorsQueryHandler(MovieManagerReferenceDataContext context)
        {
            _context = context;
        }

        public async Task<IList<ActorResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Actors
                .AsNoTracking()
                .Select(x => new ActorResponse
                {
                    ActorId = x.ActorId,
                    Name = x.Name,
                    Surname = x.Surname,
                    Age = x.Age
                })
                .OrderByDescending(x => x.ActorId)
                .ToListAsync(cancellationToken);
        }
    }
}
