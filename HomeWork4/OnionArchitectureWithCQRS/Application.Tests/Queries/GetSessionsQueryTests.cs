using Application.ProductFeatures.Queries;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace Application.Tests.Queries
{
    internal class GetSessionsQueryTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public GetSessionsQueryTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Test]
        public void DataSet_ReturnsCorrectRows()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.MovieId, 1).Create();
            var session1 = fixture.Build<Session>()
                .With(x => x.SessionId, 1)
                .With(x => x.MovieId, movie.MovieId)
                .Create();
            var session2 = fixture.Build<Session>()
                .With(x => x.SessionId, 2)
                .With(x => x.MovieId, movie.MovieId)
                .Create();
            _dbContext.AddAndSaveRange(new List<Session> { session1, session2 });

            var sessionQuery = fixture.Build<GetSessionsQuery>()
               .Create();

            _dbContext.Assert(async context => {
                // Arrange
                var sut = CreateSut(context);

                // Act
                var results = await sut.Handle(sessionQuery, _cts.Token);

                // Assert
                Assert.That(results, Is.Not.Null);
                Assert.That(results.Count, Is.EqualTo(2));

                var firstSession = results.First();
                Assert.Multiple(() =>
                {
                    Assert.That(firstSession.SessionId, Is.EqualTo(session1.SessionId));
                    Assert.That(firstSession.MovieId, Is.EqualTo(session1.MovieId));
                    Assert.That(firstSession.RoomName, Is.EqualTo(session1.RoomName));
                    Assert.That(firstSession.StartDateTime, Is.EqualTo(session1.StartDateTime));
                    Assert.That(firstSession.Movie.MovieId, Is.EqualTo(session1.Movie.MovieId));
                    Assert.That(firstSession.Movie.Title, Is.EqualTo(session1.Movie.Title));
                    Assert.That(firstSession.Movie.Description, Is.EqualTo(session1.Movie.Description));
                    Assert.That(firstSession.Movie.ReleaseDate, Is.EqualTo(session1.Movie.ReleaseDate));
                });
            });
        }

        private static GetSessionsQueryHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
