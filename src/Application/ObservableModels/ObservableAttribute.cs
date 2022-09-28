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
            set => SetProperty(Model.Value, value, Model, (model, _value) => model.Value = _value);
        }

        public int CurrentValue
        {
            get => _currentValue;
            set => SetProperty(ref _currentValue, value);
        }

        public int? MaxValue
        {
            get => Model.MaxValue;
            set => SetProperty(Model.MaxValue, value, Model, (model, _value) => model.MaxValue = _value);
        }

        public int? CurrentMaxValue
        {
            get => _currentMaxValue;
            set => SetProperty(ref _currentMaxValue, value, () => OnPropertyChanged(nameof(HasCurrentMaxValue)));
        }
        
        public bool HasCurrentValue => Value != CurrentValue;

        public bool HasCurrentMaxValue => HasMaxValue && MaxValue != CurrentMaxValue;

        public bool HasMaxValue => MaxValue.HasValue;

        public static ObservableAttribute New(string name, AttributeValue attribute) => new(name, attribute);

        public static ObservableAttribute New(string name, int value, int? maxvalue = null) => new(name, new AttributeValue()
        {
            Value = value,
            MaxValue = maxvalue
        });
    }
}