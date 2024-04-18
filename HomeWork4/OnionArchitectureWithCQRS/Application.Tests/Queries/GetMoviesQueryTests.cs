using Application.ProductFeatures.Queries;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace Application.Tests.Queries
{
    public class GetMoviesQueryTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public GetMoviesQueryTests()
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

            var movieQuery = fixture.Build<GetMoviesQuery>()           
               .Create();

            _dbContext.Assert(async context => {
                // Arrange
                var sut = CreateSut(context);

                // Act
                var results = await sut.Handle(movieQuery, _cts.Token);

                // Assert
                Assert.That(results, Is.Not.Null);
                Assert.That(results.Count, Is.EqualTo(2));

                var firstMovie = results.First();
                Assert.Multiple(() =>
                {
                    Assert.That(firstMovie.MovieId, Is.EqualTo(movie1.MovieId));
                    Assert.That(firstMovie.Title, Is.EqualTo(movie1.Title));
                    Assert.That(firstMovie.Description, Is.EqualTo(movie1.Description));
                    Assert.That(firstMovie.ReleaseDate, Is.EqualTo(movie1.ReleaseDate));
                });
            });
        }

        private static GetMoviesQueryHandler CreateSut(ApplicationDbContext context) => new(context);
    }
}
