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
    public class ObservableNoteNavigationHandlerTests
    {
        private readonly IFixture _fixture;
        public ObservableNoteNavigationHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        }

        [Fact]
        public void Constructure_WithoutINavigationService_ThrowsException()
        {
            // Arrange

            // Act
            Action action = () => { new ObservableNoteNavigationHandler(null, _fixture.Create<IAppRouting>()); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Constructure_WithoutIAppRouting_ThrowsException()
        {
            // Arrange

            // Act
            Action action = () => { new ObservableNoteNavigationHandler(_fixture.Create<INavigationService>(), null); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public async Task ACallTo_HandleWithClose_ClosesPopup()
        {
            // Arrange
            var navigationService = _fixture.Freeze<Fake<INavigationService>>();
            _ = _fixture.Freeze<Fake<IAppRouting>>();
            var handler = _fixture.Create<ObservableNoteNavigationHandler>();
            var feature = _fixture.Create<ObservableNote>();
            var request = NavRequest<ObservableNote>.Close(feature);

            // Act
            var results = await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => navigationService.FakedObject.Pop(feature)).MustHaveHappened();
            results.Should().NotBeNull();
        }

        [Fact]
        public async Task ACallTo_HandleWithIsPopupTrue_ClosesPopup()
        {
            // Arrange
            var navigationService = _fixture.Freeze<Fake<INavigationService>>();
            var appRouting = _fixture.Freeze<Fake<IAppRouting>>();
            var handler = _fixture.Create<ObservableNoteNavigationHandler>();
            var request = _fixture.Create<NavRequest<ObservableNote>>();

            A.CallTo(() => appRouting.FakedObject.IsPopup(request.Route)).Returns(true);
            A.CallTo(() => appRouting.FakedObject.GetPage(request.Route)).Returns(typeof(bool));
            A.CallTo(() => navigationService.FakedObject.PushAsync<ObservableNote>(typeof(bool), request.Paramaters))
                .Returns(_fixture.Create<ObservableNote>());

            // Act
            var results = await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => navigationService.FakedObject.PushAsync<ObservableNote>(typeof(bool), request.Paramaters)).MustHaveHappened();
            results.Should().NotBeNull();
        }

        [Fact]
        public async Task ACallTo_HandleWithIsPopupFalse_ThrowsException()
        {
            // Arrange
            _ = _fixture.Freeze<Fake<INavigationService>>();
            var appRouting = _fixture.Freeze<Fake<IAppRouting>>();
            var handler = _fixture.Create<ObservableNoteNavigationHandler>();
            var request = _fixture.Create<NavRequest<ObservableNote>>();

            A.CallTo(() => appRouting.FakedObject.IsPopup(request.Route)).Returns(false);

            // Act
            Func<Task> action = async () => { await handler.Handle(request, CancellationToken.None); };

            // Assert
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
