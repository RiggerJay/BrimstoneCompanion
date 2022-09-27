using AutoFixture.AutoFakeItEasy;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Handlers;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace Infrastructure.Tests.Handlers
{
    public class InitialiseHandlerTests
    {
        private readonly IFixture _fixture;
        public InitialiseHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        }

        [Fact]
        public void Constructure_WithoutICharacterService_ThrowsException()
        {
            // Arrange

            // Act
            Action action = () => { new InitialiseHandler(null); };
            
            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public async Task ACallTo_Handle_CallsInitialiseAsync()
        {
            // Arrange
            var service = _fixture.Freeze<Fake<ICharacterService>>();
            var handler = _fixture.Create<InitialiseHandler>();
            var request = _fixture.Create<InitialiseRequest>();

            // Act
            _ = await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => service.FakedObject.InitialiseAsync()).MustHaveHappened();
        }
    }
}
