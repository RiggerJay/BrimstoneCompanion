using CommunityToolkit.Mvvm.ComponentModel;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Services
{
    public class ApplicationState : ObservableObject, IApplicationState, IUpdateApplicationState
    {
        private readonly ICharacterService _characterService;

        private ObservableCharacter _character = ObservableCharacter.New();

        public ApplicationState(ICharacterService characterService)
        {
            _characterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
        }

        public ObservableCharacter Character
        {
            get => _character;
            private set
            {
                if (SetProperty(ref _character, value))
                {
                    OnPropertyChanged(nameof(CharacterLoaded));
                }
            }
        }

        public bool CharacterLoaded => Character is not null;

        public async Task<bool> SetCharacterAsync(ObservableCharacter character)
        {
            await _characterService.UpdateCharacterFromTemplate(character);
            Character = character;

            return CharacterLoaded;
        }
    }
}