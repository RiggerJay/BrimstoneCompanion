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
        private string _token;

        public SidebagViewModel(IMediator mediator
            , IApplicationState state)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public ObservableCharacter Character => _state.Character;

        public ObservableCollection<ObservableToken> Tokens => Character.Tokens;

        public void Reset()
        {
        }
        
        [RelayCommand(CanExecute = nameof(CanAddToken))]
        private void AddToken()
        {
            if (Tokens.Count < Character.GetAttribute(AttributeNames.SIDEBAG).MaxValue)
            {
                Tokens.Add(ObservableToken.New(Token));
            }
        }

        private bool CanAddToken() => !string.IsNullOrWhiteSpace(Token);

        [RelayCommand]
        private async Task SaveAndClose()
        {
            await _mediator.Send(NavRequest.Close(true));
        }
    }
}