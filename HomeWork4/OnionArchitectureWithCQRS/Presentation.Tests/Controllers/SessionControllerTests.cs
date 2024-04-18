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
    public class SessionControllerTests
    {
        private readonly SessionController _controller;
        private readonly Mock<IMediator> _mediatorMoq;

        public SessionControllerTests()
        {
            _mediatorMoq = new Mock<IMediator>();
            _controller = new SessionController();
            _controller.Mediator = _mediatorMoq.Object;
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetSessionAsync().Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            var fixture = new Fixture();
            var numberOfSessions = 3;
            var sessionList = fixture.Build<Session>().CreateMany(numberOfSessions).ToList();
            _mediatorMoq.Setup(x => x.Send(It.IsAny<GetSessionsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(sessionList);

            // Act
            var okResult = _controller.GetSessionAsync().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Session>>(okResult.Value);
            Assert.Equal(numberOfSessions, items.Count);
        }

        [Fact]
        public void Create_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var fixture = new Fixture();
            var sessionCommand = fixture.Build<CreateSessionCommand>()
               .Create();

            // Act
            var okResult = _controller.CreateSessionAsync(sessionCommand).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Create_WhenCalled_ReturnsRightItem()
        {
            // Arrange
            var fixture = new Fixture();
            var session = fixture.Build<Session>().Create();
            var sessionCommand = fixture.Build<CreateSessionCommand>()
               .With(x => x.SessionId, session.SessionId)
               .With(x => x.MovieId, session.MovieId)
               .With(x => x.StartDateTime, session.StartDateTime)
               .With(x => x.RoomName, session.RoomName)
               .Create();

            _mediatorMoq.Setup(x => x.Send(It.IsAny<CreateSessionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(session);
            // Act
            var okResult = _controller.CreateSessionAsync(sessionCommand).Result as OkObjectResult;

            // Assert
            Assert.IsType<Session>(okResult.Value);
            Assert.Equal(session.SessionId, (okResult.Value as Session).SessionId);
            Assert.Equal(session.MovieId, (okResult.Value as Session).MovieId);
            Assert.Equal(session.StartDateTime, (okResult.Value as Session).StartDateTime);
            Assert.Equal(session.RoomName, (okResult.Value as Session).RoomName);
        }

        [Fact]
        public void Update_UnknownIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var fixture = new Fixture();
            var sessionId = 1;
            var sessionCommand = fixture.Build<UpdateSessionCommand>()
                .With(x => x.SessionId, sessionId)
                .Create();

            var ex = fixture.Build<Exception>().Create();

            _mediatorMoq.Setup(x => x.Send(It.IsAny<UpdateSessionCommand>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

            // Act
            var notFoundResult = _controller.UpdateSessionAsync(sessionId, sessionCommand).Result;

            // Assert
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
        }

        [Fact]
        public void Update_BadIdPassed_ReturnsBadRequest()
        {
            // Arrange
            var fixture = new Fixture();
            var sessionId = 1;
            var sessionCommand = fixture.Build<UpdateSessionCommand>()
                .With(x => x.SessionId, 2)
                .Create();

            // Act
            var badResult = _controller.UpdateSessionAsync(sessionId, sessionCommand).Result;

            // Assert
            Assert.IsType<BadRequestResult>(badResult);
        }

        [Fact]
        public void Update_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var fixture = new Fixture();
            var sessionId = 1;
            var sessionCommand = fixture.Build<UpdateSessionCommand>()
               .With(x => x.SessionId, sessionId)
               .Create();

            // Act
            var okResult = _controller.UpdateSessionAsync(sessionId, sessionCommand).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Update_WhenCalled_ReturnsRightItem()
        {
            // Arrange
            var fixture = new Fixture();
            var session = fixture.Build<Session>().Create();
            var sessionCommand = fixture.Build<UpdateSessionCommand>()
               .With(x => x.SessionId, session.SessionId)
               .With(x => x.MovieId, session.MovieId)
               .With(x => x.StartDateTime, session.StartDateTime)
               .With(x => x.RoomName, session.RoomName)
               .Create();

            _mediatorMoq.Setup(x => x.Send(It.IsAny<UpdateSessionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(session);
            // Act
            var okResult = _controller.UpdateSessionAsync(session.SessionId, sessionCommand).Result as OkObjectResult;

            // Assert
            Assert.IsType<Session>(okResult.Value);
            Assert.Equal(session.SessionId, (okResult.Value as Session).SessionId);
            Assert.Equal(session.MovieId, (okResult.Value as Session).MovieId);
            Assert.Equal(session.StartDateTime, (okResult.Value as Session).StartDateTime);
            Assert.Equal(session.RoomName, (okResult.Value as Session).RoomName);
        }

    }
}
