using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableAttribute : ObservableModel<AttributeStat>
    {
        public string _name = string.Empty;

        public ObservableAttribute(string name, AttributeStat model) : base(model)
        { }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
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