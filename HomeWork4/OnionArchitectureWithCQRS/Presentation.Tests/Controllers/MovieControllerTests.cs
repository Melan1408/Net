using Application.ProductFeatures.Commands;
using Application.ProductFeatures.Queries;
using AutoFixture;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers.v1;

namespace Presentation.Tests.Controllers
{
    public class MovieControllerTests
    {
        private readonly MovieController _controller;
        private readonly Mock<IMediator> _mediatorMoq;

        public MovieControllerTests()
        {
            _mediatorMoq = new Mock<IMediator>();
            _controller = new MovieController();
            _controller.Mediator = _mediatorMoq.Object;
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetMoviesAsync().Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            var fixture = new Fixture();
            var numberOfMovies = 3;
            var movieList = fixture.Build<Movie>().CreateMany(numberOfMovies).ToList();
            _mediatorMoq.Setup(x => x.Send(It.IsAny<GetMoviesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(movieList);
            
            // Act
            var okResult = _controller.GetMoviesAsync().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Movie>>(okResult.Value);
            Assert.Equal(numberOfMovies, items.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var fixture = new Fixture();
            var movieId = 1;
            var ex = fixture.Build<Exception>().Create();

            _mediatorMoq.Setup(x => x.Send(It.IsAny<GetMovieByIdQuery>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

            // Act
            var notFoundResult = _controller.GetMovieByIdAsync(movieId).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }


        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var fixture = new Fixture();
            var movieId = 1;
            var movie = fixture.Build<Movie>()
                .With(x => x.MovieId, movieId)
                .Create();
            _mediatorMoq.Setup(x => x.Send(It.IsAny<GetMovieByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(movie);

            // Act
            var okResult = _controller.GetMovieByIdAsync(movieId).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsRightItem()
        {
            // Arrange
            var fixture = new Fixture();
            var movieId = 1;
            var movie = fixture.Build<Movie>()
                .With(x => x.MovieId, movieId)
                .Create();
            _mediatorMoq.Setup(x => x.Send(It.IsAny<GetMovieByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(movie);

            // Act
            var okResult = _controller.GetMovieByIdAsync(1).Result as OkObjectResult;

            // Assert
            Assert.IsType<Movie>(okResult.Value);
            Assert.Equal(movieId, (okResult.Value as Movie).MovieId);
        }

        [Fact]
        public void Create_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var fixture = new Fixture();
            var movieCommand = fixture.Build<CreateMovieCommand>()               
               .Create();

            // Act
            var okResult = _controller.CreateMovieAsync(movieCommand).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Create_WhenCalled_ReturnsRightItem()
        {
            // Arrange
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().Create();
            var movieCommand = fixture.Build<CreateMovieCommand>()
               .With(x => x.MovieId, movie.MovieId)
               .With(x => x.Title, movie.Title)
               .With(x => x.Description, movie.Description)
               .With(x => x.ReleaseDate, movie.ReleaseDate)
               .Create();

            _mediatorMoq.Setup(x => x.Send(It.IsAny<CreateMovieCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(movie);
            // Act
            var okResult = _controller.CreateMovieAsync(movieCommand).Result as OkObjectResult;

            // Assert
            Assert.IsType<Movie>(okResult.Value);
            Assert.Equal(movie.MovieId, (okResult.Value as Movie).MovieId);
            Assert.Equal(movie.Title, (okResult.Value as Movie).Title);
            Assert.Equal(movie.Description, (okResult.Value as Movie).Description);
            Assert.Equal(movie.ReleaseDate, (okResult.Value as Movie).ReleaseDate);
        }

        [Fact]
        public void Update_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var fixture = new Fixture();
            var movieId = 1;
            var movieCommand = fixture.Build<UpdateMovieCommand>()
                .With(x => x.MovieId, movieId)
                .Create();
            
            var ex = fixture.Build<Exception>().Create();

            _mediatorMoq.Setup(x => x.Send(It.IsAny<UpdateMovieCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

            // Act
            var notFoundResult = _controller.UpdateMovieAsync(movieId, movieCommand).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public void Update_BadIdPassed_ReturnsBadRequest()
        {
            // Arrange
            var fixture = new Fixture();
            var movieId = 1;
            var movieCommand = fixture.Build<UpdateMovieCommand>()
                .With(x => x.MovieId, 2)
                .Create();
       
            // Act
            var badResult = _controller.UpdateMovieAsync(movieId, movieCommand).Result;

            // Assert
            Assert.IsType<BadRequestResult>(badResult);
        }

        [Fact]
        public void Update_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var fixture = new Fixture();
            var movieId = 1;
            var movieCommand = fixture.Build<UpdateMovieCommand>()
               .With(x => x.MovieId, movieId)
               .Create();

            // Act
            var okResult = _controller.UpdateMovieAsync(movieId, movieCommand).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Update_WhenCalled_ReturnsRightItem()
        {
            // Arrange
            var fixture = new Fixture();
            var movie = fixture.Build<Movie>().Create();
            var movieCommand = fixture.Build<UpdateMovieCommand>()
               .With(x => x.MovieId, movie.MovieId)
               .With(x => x.Title, movie.Title)
               .With(x => x.Description, movie.Description)
               .With(x => x.ReleaseDate, movie.ReleaseDate)
               .Create();

            _mediatorMoq.Setup(x => x.Send(It.IsAny<UpdateMovieCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(movie);
            // Act
            var okResult = _controller.UpdateMovieAsync(movieCommand.MovieId, movieCommand).Result as OkObjectResult;

            // Assert
            Assert.IsType<Movie>(okResult.Value);
            Assert.Equal(movie.MovieId, (okResult.Value as Movie).MovieId);
            Assert.Equal(movie.Title, (okResult.Value as Movie).Title);
            Assert.Equal(movie.Description, (okResult.Value as Movie).Description);
            Assert.Equal(movie.ReleaseDate, (okResult.Value as Movie).ReleaseDate);
        }

        [Fact]
        public void Delete_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var fixture = new Fixture();
            var movieId = 1;
            var ex = fixture.Build<Exception>().Create();

            _mediatorMoq.Setup(x => x.Send(It.IsAny<DeleteMovieCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

            // Act
            var notFoundResult = _controller.DeleteMovieById(movieId).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public void Delete_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var movieId = 1;

            // Act
            var okResult = _controller.DeleteMovieById(movieId).Result;

            // Assert
            Assert.IsType<OkResult>(okResult as OkResult);
        }

    }
}
