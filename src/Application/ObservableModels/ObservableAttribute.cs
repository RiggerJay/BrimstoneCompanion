using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableAttribute : ObservableModel<AttributeValue>
    {
        private string _key = string.Empty;
        private bool _hasMaxValue;
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

        public int CurrentValue => Value + _parent.Features.SelectMany(x => x.Properties.Where(prop => prop.Key == Key).Select(prop => prop.Value)).Sum();

        public bool HasCurrentValue => Value != CurrentValue;

        public int? MaxValue
        {
            get => Model.MaxValue;
            set => SetProperty(Model.MaxValue, value, Model, (model, _value) => model.MaxValue = _value, MaxValueChanged);
        }

        public bool HasMaxValue => MaxValue != null;

        internal void OnValueChanged()
        {
            OnPropertyChanged(nameof(CurrentValue));
            OnPropertyChanged(nameof(HasCurrentValue));
        }

        private void MaxValueChanged()
        {
            if (MaxValue < Value)
            {
                Value = MaxValue.Value;
            }
        }

        private void ValueChanged(AttributeValue model, int value)
        {
            if (MaxValue == null || value <= MaxValue.Value)
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