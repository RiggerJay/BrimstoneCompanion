using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.Popups;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class CharacterSelectorViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository<Character> _repository;

        [ObservableProperty]
        private ObservableCharacter? _selectedCharacter;

        public CharacterSelectorViewModel(INavigationService navigationService, IRepository<Character> repository)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Title = "Character Selector";
        }

        [RelayCommand]
        private Task CreateNewCharacter() => _navigationService.NavigateToAsync("characterselector");

        [RelayCommand]
        private async Task NewCharacter()
        {
            var results = await _navigationService.PushAsync<NewCharacterPopup, ObservableCharacter?>();

            if (results != null)
            {
                Characters.Add(results);
                SelectedCharacter = results;
                await _repository.SaveAsync(results.GetModel(), results.Id);
            }
        }

        public ObservableCollection<ObservableCharacter> Characters { get; } = new();

        [RelayCommand]
        public async Task Initialise()
        {
            IsBusy = true;
            try
            {
                foreach (var character in await _repository.GetAsync())
                {
                    Characters.Add(new ObservableCharacter(character));
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(SelectedCharacter)
                && SelectedCharacter != null)
            {
                var param = new Dictionary<string, object>()
                {
                    { nameof(Character), SelectedCharacter }
                };
                await _navigationService.NavigateToAsync("character", param);
                SelectedCharacter = null;
            }
        }
    }
}