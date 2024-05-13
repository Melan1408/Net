using Microsoft.EntityFrameworkCore;
using MovieManagerReferenceData.Contract.Responses;
using MovieManagerReferenceData.Data.Context;
using MovieManagerReferenceData.Data.Entites;

namespace MovieManagerReferenceData.Service.Directors.Commands
{
    public class UpsertDirectorCommand
    {
        public int DirectorId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public Director UpsertDirector()
        {
            var director = new Director
            {
                DirectorId = DirectorId,
                Name = Name,
                Surname = Surname,
                Age = Age
            };

            return director;
        }
    }

    public class UpsertDirectorCommandHandler : IRequestHandler<UpsertDirectorCommand, DirectorResponse>
    {
        private readonly MovieManagerReferenceDataContext _context;

        public UpsertDirectorCommandHandler(MovieManagerReferenceDataContext context)
        {
            _context = context;
        }

        public async Task<DirectorResponse> Handle(UpsertDirectorCommand request, CancellationToken cancellationToken = default)
        {
            var director = await GetDirectorAsync(request.DirectorId, cancellationToken);

            if (director == null)
            {
                director = request.UpsertDirector();
                await _context.AddAsync(director, cancellationToken);
            }
            else
            {
                director.DirectorId = request.DirectorId;
                director.Name = request.Name;
                director.Surname = request.Surname;
                director.Age = request.Age;
            }
                   
            await _context.SaveChangesAsync(cancellationToken);

            return new DirectorResponse
            {
                DirectorId = director.DirectorId,
                Name = request.Name,
                Surname = request.Surname,
                Age = request.Age
            };
        }

        private async Task<Director> GetDirectorAsync(int directorId, CancellationToken cancellationToken = default)
        {
            return await _context.Directors.SingleOrDefaultAsync(x => x.DirectorId == directorId, cancellationToken);
        }
    }
}
