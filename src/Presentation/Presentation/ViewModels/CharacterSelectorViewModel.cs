using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;

namespace RedSpartan.BrimstoneCompanion.Presentation.ViewModels
{
    public partial class CharacterSelectorViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public CharacterSelectorViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        [RelayCommand]
        private Task CreateNewCharacter() => _navigationService.NavigateToAsync("main");
    }
}