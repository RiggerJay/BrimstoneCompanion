using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class LevelUpViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private ObservableCharacter _character;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveAndCloseCommand))]
        private int _requiredXP;

        [ObservableProperty]
        private ObservableAttribute _currentXP;

        public LevelUpViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ObservableCharacter Character
        {
            get => _character;
            set
            {
                if (SetProperty(ref _character, value))
                {
                    CurrentXP = _character.Attributes.First(x => x.Key == AttributeNames.XP);
                    RequiredXP = _character.Level * 500;
                }
            }
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        public async Task SaveAndClose()
        {
            if (await _mediator.Send(LevelUpCharacterRequest.With(CurrentXP, CurrentXP.Value - RequiredXP)))
            {
                await _mediator.Send(SaveCharacterRequest.Save());
                await _mediator.Send(NavRequest.Close(true));
            }
        }

        private bool CanSave() => CurrentXP?.Value >= RequiredXP;

        [RelayCommand]
        public async Task CancelAndClose() => await _mediator.Send(NavRequest.Close(false));
    }
}