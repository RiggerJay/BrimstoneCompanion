using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class SidebagViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationState _state;

        public SidebagViewModel(IMediator mediator
            , IApplicationState state)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public ObservableCharacter Character => _state.Character;

        public void Reset()
        {
        }

        [RelayCommand]
        private async Task SaveAndClose()
        {
            await _mediator.Send(NavRequest.Close(true));
        }
    }
}