using RedSpartan.BrimstoneCompanion.AppLayer.Utilities;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public partial class ObservableAttribute : ObservableModel<AttributeValue>
    {
        private readonly string _key;

        private int _currentValue;

        private int? _currentMaxValue;

        private ObservableAttribute(string key, AttributeValue model) : base(model)
        {
            _key = string.IsNullOrWhiteSpace(key) ? throw new ArgumentNullException(nameof(key)) : key;
        }

        public string Key => _key;

        public int Value
        {
            get => Model.Value;
            private set => SetProperty(Model.Value, value, Model, (model, _value) => model.Value = _value);
        }

        public int CurrentValue
        {
            get => _currentValue;
            private set => SetProperty(ref _currentValue, value, () => OnPropertyChanged(nameof(HasCurrentValue)));
        }

        public int? MaxValue
        {
            get => Model.MaxValue;
            private set => SetProperty(Model.MaxValue, value, Model, (model, _value) => model.MaxValue = _value, () => OnPropertyChanged(nameof(HasMaxValue)));
        }

        public int? CurrentMaxValue
        {
            get => _currentMaxValue;
            private set => SetProperty(ref _currentMaxValue, value, () => OnPropertyChanged(nameof(HasCurrentMaxValue)));
        }

        public bool HasCurrentValue => Value != CurrentValue;

        public bool HasCurrentMaxValue => HasMaxValue && MaxValue != CurrentMaxValue;

        public bool HasMaxValue => MaxValue.HasValue;

        public void SetValue(int value, IList<ObservableFeature> features)
        {
            if (!CurrentMaxValue.HasValue)
            {
                Value = value;
                CurrentValue = value + features.GetFeatureCount(Key);
                return;
            }

            if (CurrentMaxValue.Value >= value)
            {
                Value = value;
                CurrentValue = value;
                return;
            }

            Value = CurrentMaxValue.Value;
            CurrentValue = CurrentMaxValue.Value;
        }

        public void SetMaxValue(int value, IList<ObservableFeature> features)
        {
            MaxValue = value;
            CurrentMaxValue = value + features.GetFeatureCount(Key);

            if (CurrentMaxValue < Value)
            {
                Value = CurrentMaxValue.Value;
                CurrentValue = CurrentMaxValue.Value;
            }
        }

        public void SetCurrentValues(IList<ObservableFeature> features)
        {
            if (MaxValue.HasValue)
            {
                CurrentValue = Value;
                CurrentMaxValue = MaxValue.Value + features.GetFeatureCount(Key);
                return;
            }
            CurrentValue = Value + features.GetFeatureCount(Key);
        }

        public static ObservableAttribute New(string name, AttributeValue attribute, IList<ObservableFeature> features)
        {
            var model = new ObservableAttribute(name, attribute);
            model.SetCurrentValues(features);
            return model;
        }

        public static ObservableAttribute New(string name, int value, int? maxvalue, IList<ObservableFeature> features) =>
            New(name,
                new AttributeValue()
                {
                    Value = value,
                    MaxValue = maxvalue
                },
                features);
    }
}