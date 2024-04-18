using Application.ProductFeatures.Commands;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace Application.Tests.Commands
{
    public class CreateSessionCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public CreateSessionCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Test]
        public void AddsSession()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.MovieId, 1).Create();
            var sessionCommand = fixture.Build<CreateSessionCommand>()
                .With(x => x.SessionId, 2)
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

        private static void AssertSessionResult(CreateSessionCommand expected, Session result)
        {
            Assert.Multiple(() =>
            {
                Assert.That(result.SessionId, Is.EqualTo(expected.SessionId));
                Assert.That(result.MovieId, Is.EqualTo(expected.MovieId));
                Assert.That(result.RoomName, Is.EqualTo(expected.RoomName));
                Assert.That(result.StartDateTime, Is.EqualTo(expected.StartDateTime));
            });
        }
        private static CreateSessionCommandHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
