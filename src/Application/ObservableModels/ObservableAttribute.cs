using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableAttribute : ObservableModel<AttributeValue>
    {
        private string _key = string.Empty;
        private readonly ObservableCharacter _parent;

        private ObservableAttribute(ObservableCharacter parent, string key, AttributeValue model) : base(model)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Key = string.IsNullOrWhiteSpace(key) ? throw new ArgumentNullException(nameof(key)) : key;
        }

        public string Key
        {
            get => _key;
            set => SetProperty(ref _key, value);
        }

        public int Value
        {
            get => Model.Value;
            set => SetProperty(Model.Value, value, Model, ValueChanged, OnValueChanged);
        }

        public int CurrentValue => HasMaxValue ? Value : Value + _parent.Features.SelectMany(x => x.Properties.Where(prop => prop.Key == Key).Select(prop => prop.Value)).Sum();

        public int? CurrentMaxValue => HasMaxValue ? MaxValue + _parent.Features.SelectMany(x => x.Properties.Where(prop => prop.Key == Key).Select(prop => prop.Value)).Sum() : MaxValue;

        public bool HasCurrentValue => Value != CurrentValue;

        public bool HasCurrentMaxValue => HasMaxValue && MaxValue != CurrentMaxValue;

        public int? MaxValue
        {
            get => Model.MaxValue;
            set => SetProperty(Model.MaxValue, value, Model, (model, _value) => model.MaxValue = _value, OnMaxValueChanged);
        }

        public bool HasMaxValue => MaxValue.HasValue;

        internal void OnValueChanged()
        {
            OnPropertyChanged(nameof(CurrentValue));
            OnPropertyChanged(nameof(HasCurrentValue));
            OnMaxValueChanged();
        }

        private void OnMaxValueChanged()
        {
            if (!CurrentMaxValue.HasValue)
            {
                return;
            }
            if (CurrentMaxValue.Value < Value)
            {
                Value = CurrentMaxValue.Value;
            }
            OnPropertyChanged(nameof(CurrentMaxValue));
            OnPropertyChanged(nameof(HasCurrentMaxValue));
        }

        private void ValueChanged(AttributeValue model, int value)
        {
            if (!HasMaxValue || value <= CurrentMaxValue.Value)
            {
                model.Value = value;
            }
        }

        internal static ObservableAttribute New(ObservableCharacter parent, string name, AttributeValue attribute) => new(parent, name, attribute);

        internal static ObservableAttribute New(ObservableCharacter parent, string name, int value, int? maxvalue = null) => new(parent, name, new AttributeValue()
        {
            Value = value,
            MaxValue = maxvalue
        });
    }
}