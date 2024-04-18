using Application.ProductFeatures.Commands;
using Application.ProductFeatures.Queries;
using AutoFixture;
using Domain.Entities;
using Persistence.Context;

namespace Application.Tests.Commands
{
    public class DeleteMovieCommandTests
    {
        private readonly DbContextDecorator<ApplicationDbContext> _dbContext;
        protected readonly CancellationTokenSource _cts = new();

        public DeleteMovieCommandTests()
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
        public void DeleteMovie()
        {
            var fixture = new Fixture();

            var movieCommand = fixture.Build<DeleteMovieCommand>()
                .With(x => x.MovieId, 1)
                .Create();
            var movieQuery = fixture.Build<GetMovieByIdQuery>()
               .With(x => x.MovieId, 1)
               .Create();

            _dbContext.Assert(async context =>
            {
                // Arrange
                var sut = CreateSut(context);
                var query = CreateQuery(context);

                // Act
                await sut.Handle(movieCommand, _cts.Token);  
                var ex = Assert.ThrowsAsync<Exception>(async () => await query.Handle(movieQuery, _cts.Token));

                // Assert
                Assert.That(ex.Message, Is.EqualTo("Movie not found!"));
            });
        }

        private static DeleteMovieCommandHandler CreateSut(ApplicationDbContext context) => new(context);
        private static GetMovieByIdQueryHandler CreateQuery(ApplicationDbContext context) => new(context);
    }

}

