using Application.ProductFeatures.Queries;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace Application.Tests.Queries
{
    public class GetMovieByIdQueryTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public GetMovieByIdQueryTests()
        {
            var options = Utilities.CreateInMemoryDbOptions<ApplicationDbContext>();

            _dbContext = new DbContextDecorator<ApplicationDbContext>(options);
        }

        [Test]
        public void DataSet_ReturnsCorrectRows()
        {
            var fixture = new Fixture();
            var movie1 = fixture.Build<Movie>().With(x => x.MovieId, 1).Create();
            var movie2 = fixture.Build<Movie>().With(x => x.MovieId, 2).Create();
            _dbContext.AddAndSaveRange(new List<Movie> { movie1, movie2 });

            var movieQuery = fixture.Build<GetMovieByIdQuery>()
               .With(x => x.MovieId, 2)
               .Create();

            _dbContext.Assert(async context => {
                // Arrange
                var sut = CreateSut(context);

                // Act
                var results = await sut.Handle(movieQuery, _cts.Token);

                // Assert
                Assert.That(results, Is.Not.Null);

                var secondMovie = results;
                Assert.Multiple(() =>
                {
                    Assert.That(secondMovie.MovieId, Is.EqualTo(movie2.MovieId));
                    Assert.That(secondMovie.Title, Is.EqualTo(movie2.Title));
                    Assert.That(secondMovie.Description, Is.EqualTo(movie2.Description));
                    Assert.That(secondMovie.ReleaseDate, Is.EqualTo(movie2.ReleaseDate));
                });
            });
        }

        private static GetMovieByIdQueryHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
