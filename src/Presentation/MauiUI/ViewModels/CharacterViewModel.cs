using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using RedSpartan.BrimstoneCompanion.MauiUI.Popups;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Character), nameof(Character))]
    public partial class CharacterViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMediator _mediator;

        private ObservableCharacter _character;

        public CharacterViewModel(INavigationService navigationService
            , IMediator mediator)
        {
            Title = "Character";
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ObservableCharacter Character
        {
            get => _character;
            set
            {
                if (SetProperty(ref _character, value))
                {
                    Title = $"{_character.Name} a level {_character.Level} {_character.Class}";
                }

                Task.Run(async () => await _mediator.Send(SaveCharacterRequest.WithCharacter(_character)));
            }
        }

        [RelayCommand]
        public async Task UpdateAttribute(ObservableAttribute attribute)
        {
            if (await _navigationService.PushAsync<UpdateAttributePopup, bool>(new Dictionary<string, object> { { nameof(Attribute), attribute } }))
            {
                await _mediator.Send(SaveCharacterRequest.WithCharacter(Character));
            }
        }

        [RelayCommand]
        public async Task IncrementAttribute(ObservableAttribute attribute)
        {
            if (await _navigationService.PushAsync<IncrementAttributePopup, bool>(new Dictionary<string, object> { { nameof(Attribute), attribute } }))
            {
                await _mediator.Send(SaveCharacterRequest.WithCharacter(Character));
            }
        }

        [RelayCommand]
        public async Task DeleteCharacter()
        {
            if (await _navigationService.DisplayAlert("Are you sure?", "This will delete the character and all progress.", "Yes", "No"))
            {
                await _mediator.Send(DeleteCharacterRequest.WithCharacter(Character));
                await _navigationService.NavigateBackAsync();
            }
        }

        [RelayCommand]
        public async Task ShowNotes()
        {
            await _navigationService.NavigateToAsync("characternotes", new Dictionary<string, object>
            {
                { nameof(Character), Character }
            });
        }
    }
}