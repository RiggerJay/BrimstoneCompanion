using RedSpartan.BrimstoneCompanion.AppLayer.Resources;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableCharacter : ObservableModel<Character>
    {
        public ObservableCharacter() : this(new Character())
        { }

        public ObservableCharacter(Character character) : base(character)
        {
            foreach (var attribute in Model.Attributes)
            {
                Attributes.Add(attribute.Key, new ObservableAttribute(TextResource.ResourceManager.GetString(attribute.Key) ?? string.Empty, attribute.Value));
            }
        }

        public string Id
        {
            get => Model.Id;
            set => SetProperty(Model.Id, value, Model, (model, _value) => model.Id = _value);
        }

        public string Name
        {
            get => Model.Name;
            set => SetProperty(Model.Name, value, Model, (model, _value) => model.Name = _value);
        }

        public string Class
        {
            get => Model.Class;
            set => SetProperty(Model.Class, value, Model, (model, _value) => model.Class = _value);
        }

        public byte Level
        {
            get => Model.Level;
            set => SetProperty(Model.Level, value, Model, (model, _value) => model.Level = _value);
        }

        public IDictionary<string, ObservableAttribute> Attributes { get; } = new Dictionary<string, ObservableAttribute>();

        public ObservableAttribute AddAttribute(string key, ObservableAttribute attribute)
        {
            Attributes.Add(key, attribute);
            Model.Attributes.Add(key, attribute.GetModel());
            return attribute;
        }
    }
}