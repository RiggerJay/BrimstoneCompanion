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
using System.ComponentModel;

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
        [MemberData(nameof(TestDataGenerator.GetCurrentValueFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public async Task ACallTo_Handle_CalculatesAttributeA(Character model, int expected)
        {
            // Arrange
            var service = _fixture.Freeze<Fake<IApplicationState>>();
            var handler = _fixture.Create<UpdateAttributeValueHandler>();
            var character = ObservableCharacter.New(model);
            var attribute = character.Attributes["A"];
            using var monitor = attribute.Monitor();

            // Act
            var value = attribute.Value + 2;
            A.CallTo(() => service.FakedObject.Character).Returns(character);
            var request = UpdateAttributeValueRequest.With(attribute, value, null);

            attribute.Value.Should().Be(value - 2);

            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            attribute.Value.Should().Be(value);
            attribute.CurrentValue.Should().Be(expected + 2);
            monitor.Should().RaisePropertyChangeFor((m) => m.Value);
            monitor.Should().RaisePropertyChangeFor((m) => m.CurrentValue);
            monitor.Should().NotRaisePropertyChangeFor((m) => m.MaxValue);
            monitor.Should().NotRaisePropertyChangeFor((m) => m.CurrentMaxValue);
        }
    }
}