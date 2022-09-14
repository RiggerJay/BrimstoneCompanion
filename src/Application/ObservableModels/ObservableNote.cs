using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableNote : ObservableModel<Note>
    {
        private ObservableNote(Note model) : base(model)
        {
        }

        public string Title
        {
            get => Model.Title;
            set => SetProperty(Model.Title, value, Model, (model, _value) => model.Title = _value);
        }

        public string Body
        {
            get => Model.Body;
            set => SetProperty(Model.Body, value, Model, (model, _value) => model.Body = _value);
        }

        public DateTime Date => Model.Date;

        public static ObservableNote New(string title, string body) => new(new Note { Title = title, Body = body });

        public static ObservableNote New(string title) => new(new Note { Title = title, Body = string.Empty });

        public static ObservableNote New() => new(new Note { Title = string.Empty, Body = string.Empty });

        public static ObservableNote New(Note model) => new(model);
    }
}