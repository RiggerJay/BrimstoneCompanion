using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using Infrastructure.Tests.Data;
using RedSpartan.BrimstoneCompanion.Infrastructure.Utilities;
using FluentAssertions;

namespace Infrastructure.Tests.Utilities
{
    public class AttributeUtilitiesTests
    {
        private const string key = "A";

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCurrentValueFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void ObservableAttribute_GetCurrentValue_ReturnsExpectedValue(Character model, int expected)
        {
            // Arrange
            // Act
            var character = ObservableCharacter.New(model);

            //Assert
            character.Attributes[key].GetCurrentValue(character).Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCurrentMaxValueFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void ObservableAttribute_GetCurrentMaxValue_ReturnsExpectedValue(Character model, int expected)
        {
            // Arrange
            // Act
            var character = ObservableCharacter.New(model);

            //Assert
            character.Attributes[key].GetCurrentMaxValue(character).Should().Be(expected);
        }
    }
}