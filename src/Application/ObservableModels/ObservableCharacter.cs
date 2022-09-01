using CommunityToolkit.Mvvm.ComponentModel;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableCharacter : ObservableModel<Character>
    {
        private readonly Character _character;

        public ObservableCharacter() : this(new Character()) { }

        public ObservableCharacter(Character character) : base(character)
        { }

        public string Id
        {
            get => _character.Id;
            set => SetProperty(_character.Id, value, _character, (model, _value) => model.Id = _value);
        }

        public string Name
        {
            get => _character.Name;
            set => SetProperty(_character.Name, value, _character, (model, _value) => model.Name = _value);
        }

        public string Class
        {
            get => _character.Class;
            set => SetProperty(_character.Class, value, _character, (model, _value) => model.Class = _value);
        }
    }
}