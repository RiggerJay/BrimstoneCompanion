using CommunityToolkit.Mvvm.ComponentModel;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Services
{
    public class ApplicationState : ObservableObject, IApplicationState, IUpdateApplicationState
    {
        private ObservableCharacter _character = ObservableCharacter.New();
        private bool _characterLoaded = false;

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

        public bool UpdateCharacter(ObservableCharacter character)
        {
            Character = character;
            return CharacterLoaded = true;
        }
    }
}