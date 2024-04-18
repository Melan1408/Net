using Application.ProductFeatures.Commands;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace Application.Tests.Commands
{
    public class UpdateMovieCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public UpdateMovieCommandTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [SetUp]
        public void Init()
        {
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().With(x => x.MovieId, 1).Create();
            _dbContext.AddAndSave(movie);
        }

        [Test]
        public void UpdatesMovie()
        {
            var fixture = new Fixture();

            var movieCommand = fixture.Build<UpdateMovieCommand>()
                .With(x => x.MovieId, 1)
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


        private static void AssertMovieResult(UpdateMovieCommand expected, Movie result)
        {
            Assert.Multiple(() =>
            {
                Assert.That(result.MovieId, Is.EqualTo(expected.MovieId));
                Assert.That(result.Title, Is.EqualTo(expected.Title));
                Assert.That(result.Description, Is.EqualTo(expected.Description));
                Assert.That(result.ReleaseDate, Is.EqualTo(expected.ReleaseDate));
            });
        }

        private static UpdateMovieCommandHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
