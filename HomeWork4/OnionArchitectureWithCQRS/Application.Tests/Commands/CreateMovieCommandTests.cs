using Application.ProductFeatures.Commands;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace Application.Tests.Commands
{
    public class CreateMovieCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public CreateMovieCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }       

        [Test]
        public void AddsMovie()
        {
            var fixture = new Fixture();

            var movieCommand = fixture.Build<CreateMovieCommand>()
                .With(x => x.MovieId, 2)
                .Create();

            _dbContext.Assert(async context =>
            {
                // Arrange
                var sut = CreateSut(context);

                // Act
                var result = await sut.Handle(movieCommand, _cts.Token);

                // Assert
                Assert.That(result, Is.Not.Null);
                AssertMovieResult(movieCommand, result);
            });
        }

        private static void AssertMovieResult(CreateMovieCommand expected, Movie result)
        {
            Assert.Multiple(() =>
            {
                Assert.That(result.MovieId, Is.EqualTo(expected.MovieId));
                Assert.That(result.Title, Is.EqualTo(expected.Title));
                Assert.That(result.Description, Is.EqualTo(expected.Description));
                Assert.That(result.ReleaseDate, Is.EqualTo(expected.ReleaseDate));
            });
        }

        private static CreateMovieCommandHandler CreateSut(ApplicationDbContext context) => new(context);

    }
}
