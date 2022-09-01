using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.Popups;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
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

        [RelayCommand]
        private async Task NewCharacter()
        {
            var results = await _navigationService.PushAsync<NewCharacterPopup, ObservableCharacter?>();

            if (results != null)
            {
                Characters.Add(results);
            }
        }

        public ObservableCollection<ObservableCharacter> Characters { get; } = new();
    }
}