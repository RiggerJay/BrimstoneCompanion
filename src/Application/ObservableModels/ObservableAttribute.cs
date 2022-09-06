using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableAttribute : ObservableModel<AttributeStat>
    {
        public string _key = string.Empty;

        public ObservableAttribute(string key, AttributeStat model) : base(model)
        {
            Key = key;
        }

        public string Key
        {
            get => _key;
            set => SetProperty(ref _key, value);
        }

        public int Value
        {
            get => Model.Value;
            set => SetProperty(Model.Value, value, Model, (model, _value) => model.Value = _value);
        }

        public int? MaxValue
        {
            get => Model.MaxValue;
            set => SetProperty(Model.MaxValue, value, Model, (model, _value) => model.MaxValue = _value);
        }

        public static ObservableAttribute New(string name, int value, int? maxvalue = null) => new(name, new AttributeStat()
        {
            Value = value,
            MaxValue = maxvalue
        });
    }
}