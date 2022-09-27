using AutoFixture.AutoFakeItEasy;
using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Handlers;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace Infrastructure.Tests.Handlers
{
    public class CreateCharacterHandlerTests
    {
        private readonly IFixture _fixture;
        public CreateCharacterHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        }

        [Fact]
        public void Constructure_WithoutICharacterService_ThrowsException()
        {
            // Arrange

            // Act
            Action action = () => { new CreateCharacterHandler(null); };
            
            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public async Task ACallTo_Handle_CallsCreateAsync()
        {
            // Arrange
            var service = _fixture.Freeze<Fake<ICharacterService>>();
            var handler = _fixture.Create<CreateCharacterHandler>();
            var request = _fixture.Create<CreateCharacterRequest>();

            // Act
            _ = await handler.Handle(request, CancellationToken.None);

            // Assert
            A.CallTo(() => service.FakedObject.CreateAsync(request.Name, request.Role)).MustHaveHappened();
        }
    }
}
