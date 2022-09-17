using CommunityToolkit.Mvvm.ComponentModel;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Services
{
    public class ApplicationState : ObservableObject, IApplicationState, IUpdateApplicationState
    {
        private ObservableCharacter? _character;

        public ObservableCharacter? Character
        {
            get => _character;
            private set => SetProperty(ref _character, value);
        }

        public bool UpdateCharacter(ObservableCharacter character)
        {
            Character = character;

            return true;
        }
    }
}