using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using FakeItEasy;
using FluentAssertions;
using Infrastructure.Tests.Data;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.Infrastructure.Handlers;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace Infrastructure.Tests.Handlers
{
    public class UpdateAttributeValueHandlerTests
    {
        private readonly IFixture _fixture;

        public UpdateAttributeValueHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
        }

        [Fact]
        public void Constructure_WithoutIApplicationState_ThrowsException()
        {
            // Arrange

            // Act
            Action action = () => { new UpdateAttributeValueHandler(null); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCurrentValueWithMax), MemberType = typeof(TestDataGenerator))]
        public async Task ACallTo_Handle_CalculatesAttributeAWithoutMaxValue(Character model, int add, int expectedValue, int expectedCurrentValue)
        {
            // Arrange
            var service = _fixture.Freeze<Fake<IApplicationState>>();
            var handler = _fixture.Create<UpdateAttributeValueHandler>();
            var character = ObservableCharacter.New(model);
            var attribute = character.Attributes["A"];

            // Act
            var value = attribute.Value + add;
            A.CallTo(() => service.FakedObject.Character).Returns(character);
            var request = UpdateAttributeValueRequest.With(attribute, value);
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            attribute.Value.Should().Be(expectedValue);
            attribute.CurrentValue.Should().Be(expectedCurrentValue);
        }
    }
}