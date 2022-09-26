using FluentAssertions;
using Infrastructure.Tests.Data;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.Infrastructure.Utilities;

namespace Infrastructure.Tests.Utilities
{
    public class CharacterUtilitiesTests
    {
        private const string key = "A";

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCurrentValueFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void ObservableCharacter_UpdateAttributeCurrentValues_ReturnsExpectedValue(Character model, int expected)
        {
            // Arrange
            // Act
            var character = ObservableCharacter.New(model);

            // Assert
            character.Attributes[key].CurrentValue.Should().Be(0);
            character.UpdateAttributeCurrentValues(key).Attributes[key].CurrentValue.Should().Be(expected);
        }
    }
}