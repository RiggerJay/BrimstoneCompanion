using AutoFixture.AutoFakeItEasy;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Handlers;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace Infrastructure.Tests.Handlers
{
    public class QueryCharacterHandlerTests
    {
        private readonly IFixture _fixture;

        public QueryCharacterHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        }

        [Fact]
        public void Constructure_WithoutICharacterService_ThrowsException()
        {
            // Arrange

            // Act
            Action action = () => { new QueryCharacterHandler(null); };
            
            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public async Task ACallTo_Handle_CallsGetAllAsync()
        {
            // Arrange
            var service = _fixture.Freeze<Fake<ICharacterService>>();
            var handler = _fixture.Create<QueryCharacterHandler>();
            var request = _fixture.Create<QueryCharacterRequest>();

            // Act
            _ = await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => service.FakedObject.GetAllAsync()).MustHaveHappened();
        }
    }
}
