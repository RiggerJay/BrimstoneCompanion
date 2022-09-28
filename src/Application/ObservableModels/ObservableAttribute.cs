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
            private set => SetProperty(ref _currentValue, value);
        }

        public int? MaxValue
        {
            get => Model.MaxValue;
            private set => SetProperty(Model.MaxValue, value, Model, (model, _value) => model.MaxValue = _value);
        }

        public int? CurrentMaxValue
        {
            get => _currentMaxValue;
            private set => SetProperty(ref _currentMaxValue, value, () => OnPropertyChanged(nameof(HasCurrentMaxValue)));
        }
                public bool HasCurrentValue => Value != CurrentValue;

        public bool HasCurrentMaxValue => HasMaxValue && MaxValue != CurrentMaxValue;

        public bool HasMaxValue => MaxValue.HasValue;

        public void SetValue(int value, int currentValue)
        {
            if (!MaxValue.HasValue
                || MaxValue > value)
            {
                Value = value;
            }
            if (!CurrentMaxValue.HasValue
                || CurrentMaxValue > currentValue)
            {
                CurrentValue = currentValue;
            }
        }

        public void SetMaxValue(int? maxValue, int? currentMaxValue)
        {
            MaxValue = maxValue;
            CurrentMaxValue = currentMaxValue;
            //TODO: fix this
            if (MaxValue.HasValue && MaxValue > Value)
            {
                Value = MaxValue.Value;
            }

            if (CurrentMaxValue.HasValue && CurrentMaxValue > Value)
            {
                CurrentValue = CurrentMaxValue.Value;
            }
        }

        public static ObservableAttribute New(string name, AttributeValue attribute) => new(name, attribute);

        public static ObservableAttribute New(string name, int value, int? maxvalue = null) => new(name, new AttributeValue()
        {
            Value = value,
            MaxValue = maxvalue
        });
    }
}