using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using RedSpartan.BrimstoneCompanion.MauiUI.Popups;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class CharacterSelectorViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMediator _mediator;

        [ObservableProperty]
        private ObservableCharacter? _selectedCharacter;

        public CharacterSelectorViewModel(INavigationService navigationService
            , IMediator mediator)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
                await _mediator.Send(SaveCharacterRequest.WithCharacter(results));
            }
        }

        public ObservableCollection<ObservableCharacter> Characters { get; set; } = new();

        [RelayCommand]
        public async Task Initialise()
        {
            IsBusy = true;
            try
            {
                Characters = await _mediator.Send(QueryCharacterRequest.All());
                OnPropertyChanged(nameof(Characters));
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