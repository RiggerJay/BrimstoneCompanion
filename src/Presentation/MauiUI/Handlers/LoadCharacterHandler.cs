using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class LoadCharacterHandler : IRequestHandler<LoadCharacterRequest>
    {
        private readonly INavigationService _service;
        private readonly IUpdateApplicationState _updateApplicationState;

        public LoadCharacterHandler(INavigationService service, IUpdateApplicationState updateApplicationState)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _updateApplicationState = updateApplicationState ?? throw new ArgumentNullException(nameof(updateApplicationState));
        }

        public async Task<Unit> Handle(LoadCharacterRequest request, CancellationToken cancellationToken)
        {
            _updateApplicationState.UpdateCharacter(request.Character);

            await _service.NavigateToAsync(NavigationKeys.BACK);

            return Unit.Value;
        }
    }
}