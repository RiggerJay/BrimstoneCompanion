using CommunityToolkit.Mvvm.ComponentModel;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Services
{
    public class ApplicationState : ObservableObject, IApplicationState, IUpdateApplicationState
    {
        private readonly ICharacterService _characterService;
        private ObservableCharacter _character = ObservableCharacter.New();
        private bool _characterLoaded = false;

        public ApplicationState(ICharacterService characterService)
        {
            _characterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
        }

        public ObservableCharacter Character
        {
            get => _character;
            private set => SetProperty(ref _character, value);
        }

        public bool CharacterLoaded
        {
            get => _characterLoaded;
            private set => SetProperty(ref _characterLoaded, value);
        }

        public async Task<bool> UpdateCharacterAsync(ObservableCharacter character)
        {
            if (await _characterService.UpdateAsync(character))
            {
                Character = character;
                return CharacterLoaded = true;
            }

            return false;
        }
    }
}