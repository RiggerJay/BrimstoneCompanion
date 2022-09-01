using CommunityToolkit.Mvvm.ComponentModel;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableCharacter : ObservableObject
    {
        private readonly Character _character;

        public ObservableCharacter() : this(new Character())
        {
        }

        public ObservableCharacter(Character character)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));
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