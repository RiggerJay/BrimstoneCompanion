using CommunityToolkit.Mvvm.Messaging;
using RedSpartan.BrimstoneCompanion.Infrastructure.Messages;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel(IMessenger messenger)
        {
            if (messenger == null)
            {
                throw new ArgumentNullException(nameof(messenger));
            }

            messenger.Register<CharacterLoaded>(this, HandleCharacterLoaded);
        }

        private bool _showSelectorTab = true;

        public bool ShowSelectorTab
        {
            get => _showSelectorTab;
            private set => SetProperty(ref _showSelectorTab, value);
        }


        private void HandleCharacterLoaded(object recipient, CharacterLoaded message)
        {
            ShowSelectorTab = !message.Success;
        }
    }
}