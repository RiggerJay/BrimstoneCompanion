using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.Presentation.ViewModels
{
    public partial class CharacterSelectorViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCharacter? _selectedCharacter;

        public CharacterSelectorViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        [RelayCommand]
        private Task CreateNewCharacter() => _navigationService.NavigateToAsync("main");

        public ObservableCollection<ObservableCharacter> Characters { get; } = new();
    }
}