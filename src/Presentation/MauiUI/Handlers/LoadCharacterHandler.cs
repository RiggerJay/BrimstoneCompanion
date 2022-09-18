using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class LoadCharacterHandler : IRequestHandler<LoadCharacterRequest>
    {
        private readonly INavigationService _service;
        private readonly IUpdateApplicationState _updateApplicationState;
        private readonly ShellViewModel _viewModel;

        public LoadCharacterHandler(INavigationService service
            , IUpdateApplicationState updateApplicationState
            , ShellViewModel viewModel)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _updateApplicationState = updateApplicationState ?? throw new ArgumentNullException(nameof(updateApplicationState));
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public async Task<Unit> Handle(LoadCharacterRequest request, CancellationToken cancellationToken)
        {
            _viewModel.ShowSelectorTab = false;
            _updateApplicationState.UpdateCharacter(request.Character);

            await _service.NavigateToAsync("//tabbar/character");

            return Unit.Value;
        }
    }
}