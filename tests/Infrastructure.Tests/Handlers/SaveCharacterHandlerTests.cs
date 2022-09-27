using AutoFixture.AutoFakeItEasy;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Handlers;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace Infrastructure.Tests.Handlers
{
    public class SaveCharacterHandlerTests
    {
        private readonly IFixture _fixture;

        public SaveCharacterHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        }

        [Fact]
        public void Constructure_WithoutICharacterService_ThrowsException()
        {
            // Arrange
            var state = _fixture.Freeze<Fake<IApplicationState>>();

            // Act
            Action action = () => { new SaveCharacterHandler(null, state.FakedObject); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Constructure_WithoutIApplicationState_ThrowsException()
        {
            // Arrange
            var service = _fixture.Freeze<Fake<ICharacterService>>();

            // Act
            Action action = () => { new SaveCharacterHandler(service.FakedObject, null); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public async Task ACallTo_Handle_CallsGetAllAsync()
        {
            // Arrange
            var service = _fixture.Freeze<Fake<ICharacterService>>();
            var state = _fixture.Freeze<Fake<IApplicationState>>();
            var handler = _fixture.Create<SaveCharacterHandler>();
            var request = _fixture.Create<SaveCharacterRequest>();
            var character = _fixture.Create<ObservableCharacter>();

            // Act
            A.CallTo(() => state.FakedObject.Character).Returns(character);
            _ = await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => service.FakedObject.SaveAsync(character)).MustHaveHappened();
        }
    }
}