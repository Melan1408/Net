using Application.ProductFeatures.Commands;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace Application.Tests.Commands
{
    public class UpdateSessionCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public UpdateSessionCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [SetUp]
        public void Init()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.MovieId, 1).Create();
            var session = fixture.Build<Session>()
               .With(x => x.SessionId, 1)
               .With(x => x.MovieId, movie.MovieId)
               .Create();
            _dbContext.AddAndSave(session);
        }

        [Test]
        public void UpdatesSession()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.MovieId, 2).Create();
            var sessionCommand = fixture.Build<UpdateSessionCommand>()
                .With(x => x.SessionId, 1)
                .With(x => x.MovieId, movie.MovieId)
                .Create();

            _dbContext.Assert(async context =>
            {
                // Arrange
                var sut = CreateSut(context);

                // Act
                var result = await sut.Handle(sessionCommand, _cts.Token);

                // Assert
                Assert.That(result, Is.Not.Null);
                AssertSessionResult(sessionCommand, result);
            });
        }

        private static void AssertSessionResult(UpdateSessionCommand expected, Session result)
        {
            Assert.Multiple(() =>
            {
                Assert.That(result.SessionId, Is.EqualTo(expected.SessionId));
                Assert.That(result.MovieId, Is.EqualTo(expected.MovieId));
                Assert.That(result.RoomName, Is.EqualTo(expected.RoomName));
                Assert.That(result.StartDateTime, Is.EqualTo(expected.StartDateTime));
            });
        }
        private static UpdateSessionCommandHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
