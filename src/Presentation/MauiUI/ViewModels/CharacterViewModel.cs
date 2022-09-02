using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Character), nameof(Character))]
    public partial class CharacterViewModel : ViewModelBase
    {
        private ObservableCharacter _character;

        public CharacterViewModel()
        {
            Title = "Character";
        }

        public ObservableCharacter Character
        {
            get => _character;
            set
            {
                if (SetProperty(ref _character, value))
                {
                    Title = $"{_character.Name} a level {_character.Level} {_character.Class}";
                }
            }
        }
    }
}