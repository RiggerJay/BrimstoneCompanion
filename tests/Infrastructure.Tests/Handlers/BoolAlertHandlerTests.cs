using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using FakeItEasy;
using FluentAssertions;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Handlers;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace Infrastructure.Tests.Handlers
{
    public class BoolAlertHandlerTests
    {
        private readonly IFixture _fixture;
        public BoolAlertHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        }

        [Fact]
        public void Constructure_WithoutIAlertService_ThrowsException()
        {
            // Arrange

            // Act
            Action action = () => { new BoolAlertHandler(null); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public async Task ACallTo_Handle_CallsDisplayAlert()
        {
            // Arrange
            var alertService = _fixture.Freeze<Fake<IAlertService>>();
            var handler = _fixture.Create<BoolAlertHandler>();
            var request = _fixture.Create<BoolAlertRequest>();

            // Act
            _ = await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => alertService.FakedObject.DisplayAlert(request.Title, request.Message, request.Accept, request.Cancel)).MustHaveHappened();
        }
    }
}
