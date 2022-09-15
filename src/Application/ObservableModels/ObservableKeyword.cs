using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableKeyword : ObservableModel<Keyword>
    {
        private ObservableKeyword(Keyword model) : base(model)
        {
        }

        public string Word
        {
            get => Model.Word;
            set => SetProperty(Model.Word, value, Model, (model, _value) => model.Word = _value);
        }

        public bool CanDelete => Model.CanDelete;

        public static ObservableKeyword New(string keyword, bool canDelete = false) => new(new Keyword { Word = keyword, CanDelete = canDelete });

        public static ObservableKeyword New(Keyword keyword) => new(keyword);
    }
}