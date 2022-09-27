using AutoFixture.AutoFakeItEasy;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Handlers;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace Infrastructure.Tests.Handlers
{
    public class DeleteCharacterHandlerTests
    {
        private readonly IFixture _fixture;
        public DeleteCharacterHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        }

        [Fact]
        public void Constructure_WithoutICharacterService_ThrowsException()
        {
            // Arrange

            // Act
            Action action = () => { new DeleteCharacterHandler(null); };
            
            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public async Task ACallTo_Handle_CallsDeleteAsync()
        {
            // Arrange
            var service = _fixture.Freeze<Fake<ICharacterService>>();
            var handler = _fixture.Create<DeleteCharacterHandler>();
            var request = _fixture.Create<DeleteCharacterRequest>();

            // Act
            _ = await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => service.FakedObject.DeleteAsync(request.Character)).MustHaveHappened();
        }
    }
}
