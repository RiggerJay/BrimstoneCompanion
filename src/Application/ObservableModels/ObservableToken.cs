using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableToken : ObservableModel<Token>
    {
        private ObservableToken(Token model) : base(model) { }

        public ObservableToken(string name) : this(new Token { Name = name }) { }

        public string Name
        {
            get => Model.Name;
            set => SetProperty(Model.Name, value, Model, (model, _value) => model.Name = _value);
        }

        public string Description
        {
            get => Model.Description;
            set => SetProperty(Model.Description, value, Model, (model, _value) => model.Description = _value);
        }

        public static ObservableToken New(string token) => new (token);

        internal static ObservableToken New(Token token) => new (token);
    }
}