using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableAttribute : ObservableModel<AttributeStat>
    {
        public ObservableAttribute(AttributeStat model) : base(model)
        { }

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

        public static ObservableAttribute New(int value, int? maxvalue = null) => new(new AttributeStat()
        {
            Value = value,
            MaxValue = maxvalue
        });
    }
}