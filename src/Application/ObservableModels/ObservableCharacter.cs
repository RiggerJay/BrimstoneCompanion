using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableCharacter
    {
        private readonly Character _character;

        public ObservableCharacter(Character character)
        {
            _character = character ?? throw new ArgumentNullException(nameof(character));
        }

        public string Name
        {
            get => _character.Name;
            set => _character.Name = value;
        }

        public string Class
        {
            get => _character.Class;
            set => _character.Class = value;
        }
    }
}