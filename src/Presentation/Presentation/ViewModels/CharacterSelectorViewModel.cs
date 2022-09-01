using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;

namespace RedSpartan.BrimstoneCompanion.Presentation.ViewModels
{
    public class CharacterSelectorViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public CharacterSelectorViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }
    }
}