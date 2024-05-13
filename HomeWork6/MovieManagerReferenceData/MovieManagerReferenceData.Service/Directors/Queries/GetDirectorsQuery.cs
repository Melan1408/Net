using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Contract.Responses;
using MovieManagerReferenceData.Data.Context;

namespace MovieManagerReferenceData.Service.Directors.Queries
{
    public class GetDirectorsQueryHandler : IRequestHandler<IList<DirectorResponse>>
    {
        private readonly MovieManagerReferenceDataContext _context;

        public GetDirectorsQueryHandler(MovieManagerReferenceDataContext context)
        {
            _context = context;
        }

        public async Task<IList<DirectorResponse>> Handle(CancellationToken cancellationToken = default)
        {
            return await _context.Directors
                .AsNoTracking()
                .Select(x => new DirectorResponse
                {
                    DirectorId = x.DirectorId,
                    Name = x.Name,
                    Surname = x.Surname,
                    Age = x.Age
                })
                .OrderByDescending(x => x.DirectorId)
                .ToListAsync(cancellationToken);
        }
    }
}
