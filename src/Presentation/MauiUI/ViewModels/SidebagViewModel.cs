using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class SidebagViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationState _state;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddTokenCommand))]
        private string _token = string.Empty;

        public SidebagViewModel(IMediator mediator
            , IApplicationState state)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public ObservableCharacter Character => _state.Character;

        public ObservableCollection<ObservableToken> Tokens => Character.Tokens;

        public ObservableAttribute SidebagAttribute => Character.Attributes.First(x => x.Key == AttributeNames.SIDEBAG);

        public bool NotAtCapacity => Tokens.Count < SidebagAttribute.CurrentMaxValue;
        public bool AtCapacity => !NotAtCapacity;

        public void Reset()
        {
        }

        [RelayCommand(CanExecute = nameof(CanAddToken))]
        private async Task AddToken()
        {
            if (NotAtCapacity)
            {
                Tokens.Add(ObservableToken.New(Token));
                await _mediator.Send(SaveCharacterRequest.Save());
                Token = string.Empty;
                OnPropertyChanged(nameof(NotAtCapacity));
                OnPropertyChanged(nameof(AtCapacity));
            }
        }

        private bool CanAddToken() => NotAtCapacity
            && !string.IsNullOrWhiteSpace(Token);

        [RelayCommand]
        private async Task DeleteToken(ObservableToken token)
        {
            Tokens.Remove(token);
            await _mediator.Send(SaveCharacterRequest.Save());
            OnPropertyChanged(nameof(NotAtCapacity));
            OnPropertyChanged(nameof(AtCapacity));
        }

        [RelayCommand]
        private async Task SaveAndClose()
        {
            await _mediator.Send(NavRequest.Close(true));
        }
    }
}