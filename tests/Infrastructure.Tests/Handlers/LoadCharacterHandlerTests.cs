using AutoFixture.AutoFakeItEasy;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Handlers;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;
using CommunityToolkit.Mvvm.Messaging;
using RedSpartan.BrimstoneCompanion.Infrastructure.Messages;
using MediatR;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Infrastructure.Tests.Handlers
{
    public class LoadCharacterHandlerTests
    {
        private readonly IFixture _fixture;

        public LoadCharacterHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        }

        [Fact]
        public void Constructure_WithoutINavigationService_ThrowsException()
        {
            // Arrange
            var updateApplicationState = _fixture.Freeze<Fake<IUpdateApplicationState>>();
            var messenger = _fixture.Freeze<Fake<IMessenger>>();

            // Act
            Action action = () => { new LoadCharacterHandler(null, updateApplicationState.FakedObject, messenger.FakedObject); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Constructure_WithoutIUpdateApplicationState_ThrowsException()
        {
            // Arrange
            var navigationService = _fixture.Freeze<Fake<INavigationService>>();
            var messenger = _fixture.Freeze<Fake<IMessenger>>();

            // Act
            Action action = () => { new LoadCharacterHandler(navigationService.FakedObject, null, messenger.FakedObject); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Constructure_WithoutIMessenger_ThrowsException()
        {
            // Arrange
            var navigationService = _fixture.Freeze<Fake<INavigationService>>();
            var updateApplicationState = _fixture.Freeze<Fake<IUpdateApplicationState>>();

            // Act
            Action action = () => { new LoadCharacterHandler(navigationService.FakedObject, updateApplicationState.FakedObject, null); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public async Task ACallTo_Handle_WhereUpdateCharacterAsyncReturnsTrue()
        {
            // Arrange
            var navigationService = _fixture.Freeze<Fake<INavigationService>>();
            var updateApplicationState = _fixture.Freeze<Fake<IUpdateApplicationState>>();
            var messenger = _fixture.Freeze<Fake<IMessenger>>();
            var handler = _fixture.Create<LoadCharacterHandler>();
            var request = _fixture.Create<LoadCharacterRequest>();

            // Act
            A.CallTo(() => updateApplicationState.FakedObject.SetCharacterAsync(request.Character)).Returns(true);

            _ = await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => navigationService.FakedObject.NavigateToAsync("//tabbar/character", null)).MustHaveHappened();
        }

        [Fact]
        public async Task ACallTo_Handle_WhereUpdateCharacterAsyncReturnsFalse()
        {
            // Arrange
            var navigationService = _fixture.Freeze<Fake<INavigationService>>();
            var updateApplicationState = _fixture.Freeze<Fake<IUpdateApplicationState>>();
            var messenger = _fixture.Freeze<Fake<IMessenger>>();
            var handler = _fixture.Create<LoadCharacterHandler>();
            var request = _fixture.Create<LoadCharacterRequest>();

            // Act
            A.CallTo(() => updateApplicationState.FakedObject.SetCharacterAsync(request.Character)).Returns(false);

            _ = await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => navigationService.FakedObject.NavigateToAsync("//tabbar/character", null)).MustNotHaveHappened();
        }
    }
}