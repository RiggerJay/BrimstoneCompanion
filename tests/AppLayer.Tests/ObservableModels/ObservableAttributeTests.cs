using AppLayer.Tests.Data;
using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using FluentAssertions;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace AppLayer.Tests.ObservableModels
{
    public class ObservableAttributeTests
    {
        private readonly IFixture _fixture;
        private readonly AttributeValue _attribute;
        private const string _key = "A";
        private const int _value = 20;
        private const int _maxValue = 20;

        public ObservableAttributeTests()
        {
            _fixture = new Fixture().Customize(new AutoFakeItEasyCustomization());
            _attribute = _fixture.Create<AttributeValue>();
        }

        [Fact]
        public void Constructor_WithoutKey_ThrowsException()
        {
            // Arrange

            // Act
            Action action = () => { ObservableAttribute.New(null, _attribute); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_WithEmptyKey_ThrowsException()
        {
            // Arrange
            var attribute = _fixture.Create<AttributeValue>();

            // Act
            Action action = () => { ObservableAttribute.New(string.Empty, _attribute); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_WithoutAttribute_ThrowsException()
        {
            // Arrange

            // Act
            Action action = () => { ObservableAttribute.New(_key, null); };

            // Assert
            action.Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetFeatures), MemberType = typeof(TestDataGenerator))]
        public void SetCurrentValues_WithoutMaxValue_ReturnsAsExpected(IList<Feature> list, int total)
        {
            // Arrange
            _attribute.MaxValue = null;
            _attribute.Value = _value;
            var attribute = ObservableAttribute.New(_key, _attribute);
            var monitor = attribute.Monitor();

            // Act
            var features = list.Select(x => ObservableFeature.New(x)).ToList();
            attribute.SetCurrentValues(features);

            // Assert
            attribute.CurrentValue.Should().Be(_value + total);
            attribute.CurrentMaxValue.Should().BeNull();
            monitor.Should().RaisePropertyChangeFor(x => x.CurrentValue);
            monitor.Should().RaisePropertyChangeFor(x => x.HasCurrentValue);
            monitor.Should().NotRaisePropertyChangeFor(x => x.CurrentMaxValue);
            monitor.Should().NotRaisePropertyChangeFor(x => x.HasCurrentMaxValue);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetFeatures), MemberType = typeof(TestDataGenerator))]
        public void SetCurrentValues_WithMaxValue_ReturnsAsExpected(IList<Feature> list, int total)
        {
            // Arrange
            _attribute.MaxValue = _maxValue;
            _attribute.Value = _value;
            var attribute = ObservableAttribute.New(_key, _attribute);
            using var monitor = attribute.Monitor();

            // Act
            var features = list.Select(x => ObservableFeature.New(x)).ToList();
            attribute.SetCurrentValues(features);

            // Assert
            attribute.CurrentValue.Should().Be(_value);
            attribute.CurrentMaxValue.Should().Be(_maxValue + total);
            monitor.Should().RaisePropertyChangeFor(x => x.CurrentValue);
            monitor.Should().RaisePropertyChangeFor(x => x.HasCurrentValue);
            monitor.Should().RaisePropertyChangeFor(x => x.CurrentMaxValue);
            monitor.Should().RaisePropertyChangeFor(x => x.HasCurrentMaxValue);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetFeatures), MemberType = typeof(TestDataGenerator))]
        public void SetValue_WithoutMaxValue_ReturnsAsExpected(IList<Feature> list, int total)
        {
            // Arrange
            _attribute.MaxValue = null;
            var attribute = ObservableAttribute.New(_key, _attribute);
            using var monitor = attribute.Monitor();

            // Act
            var features = list.Select(x => ObservableFeature.New(x)).ToList();
            attribute.SetValue(_value, features);

            // Assert
            attribute.Value.Should().Be(_value);
            attribute.CurrentValue.Should().Be(_value + total);
            attribute.CurrentMaxValue.Should().BeNull();
            monitor.Should().RaisePropertyChangeFor(x => x.Value);
            monitor.Should().RaisePropertyChangeFor(x => x.CurrentValue);
            monitor.Should().RaisePropertyChangeFor(x => x.HasCurrentValue);
            monitor.Should().NotRaisePropertyChangeFor(x => x.CurrentMaxValue);
            monitor.Should().NotRaisePropertyChangeFor(x => x.HasCurrentMaxValue);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetFeatures), MemberType = typeof(TestDataGenerator))]
        public void SetValue_WithMaxValue_ReturnsValueAsMaxValue(IList<Feature> list, int total)
        {
            // Arrange
            _attribute.MaxValue = 1;
            var attribute = ObservableAttribute.New(_key, _attribute);
            var features = list.Select(x => ObservableFeature.New(x)).ToList();
            attribute.SetCurrentValues(features);
            using var monitor = attribute.Monitor();

            // Act
            attribute.SetValue(_value, features);

            // Assert
            attribute.Value.Should().Be(1 + total);
            attribute.CurrentValue.Should().Be(1 + total);
            attribute.MaxValue.Should().Be(1);
            attribute.CurrentMaxValue.Should().Be(1 + total);
            monitor.Should().RaisePropertyChangeFor(x => x.Value);
            monitor.Should().RaisePropertyChangeFor(x => x.CurrentValue);
            monitor.Should().RaisePropertyChangeFor(x => x.HasCurrentValue);
            monitor.Should().NotRaisePropertyChangeFor(x => x.MaxValue);
            monitor.Should().NotRaisePropertyChangeFor(x => x.HasMaxValue);
            monitor.Should().NotRaisePropertyChangeFor(x => x.CurrentMaxValue);
            monitor.Should().NotRaisePropertyChangeFor(x => x.HasCurrentMaxValue);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetFeatures), MemberType = typeof(TestDataGenerator))]
        public void SetMaxValue_WithValue_ReturnsValueAsExpected(IList<Feature> list, int total)
        {
            // Arrange
            _attribute.Value = _value;
            _attribute.MaxValue = 10;
            var attribute = ObservableAttribute.New(_key, _attribute);
            var features = list.Select(x => ObservableFeature.New(x)).ToList();
            attribute.SetCurrentValues(features);
            using var monitor = attribute.Monitor();

            // Act
            attribute.SetMaxValue(_maxValue, features);

            // Assert
            attribute.Value.Should().Be(_value);
            attribute.CurrentValue.Should().Be(_value);
            attribute.MaxValue.Should().Be(_maxValue);
            attribute.CurrentMaxValue.Should().Be(_maxValue + total);
            monitor.Should().NotRaisePropertyChangeFor(x => x.Value);
            monitor.Should().NotRaisePropertyChangeFor(x => x.CurrentValue);
            monitor.Should().NotRaisePropertyChangeFor(x => x.HasCurrentValue);
            monitor.Should().RaisePropertyChangeFor(x => x.MaxValue);
            monitor.Should().RaisePropertyChangeFor(x => x.HasMaxValue);
            monitor.Should().RaisePropertyChangeFor(x => x.CurrentMaxValue);
            monitor.Should().RaisePropertyChangeFor(x => x.HasCurrentMaxValue);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetFeatures), MemberType = typeof(TestDataGenerator))]
        public void SetMaxValue_WithLowValue_ReturnsValueAsMaxCurrentValue(IList<Feature> list, int total)
        {
            // Arrange
            _attribute.Value = _value;
            _attribute.MaxValue = _maxValue;
            var attribute = ObservableAttribute.New(_key, _attribute);
            var features = list.Select(x => ObservableFeature.New(x)).ToList();
            attribute.SetCurrentValues(features);
            using var monitor = attribute.Monitor();

            // Act
            attribute.SetMaxValue(1, features);

            // Assert
            attribute.Value.Should().Be(1 + total);
            attribute.CurrentValue.Should().Be(1 + total);
            attribute.MaxValue.Should().Be(1);
            attribute.CurrentMaxValue.Should().Be(1 + total);
            monitor.Should().RaisePropertyChangeFor(x => x.Value);
            monitor.Should().RaisePropertyChangeFor(x => x.CurrentValue);
            monitor.Should().RaisePropertyChangeFor(x => x.HasCurrentValue);
            monitor.Should().RaisePropertyChangeFor(x => x.MaxValue);
            monitor.Should().RaisePropertyChangeFor(x => x.HasMaxValue);
            monitor.Should().RaisePropertyChangeFor(x => x.CurrentMaxValue);
            monitor.Should().RaisePropertyChangeFor(x => x.HasCurrentMaxValue);
        }
    }
}