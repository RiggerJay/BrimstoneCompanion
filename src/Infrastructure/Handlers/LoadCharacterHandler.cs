using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Messages;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class LoadCharacterHandler : IRequestHandler<LoadCharacterRequest>
    {
        private readonly INavigationService _service;
        private readonly IUpdateApplicationState _updateApplicationState;
        private readonly IMessenger _messenger;

        public LoadCharacterHandler(INavigationService service
            , IUpdateApplicationState updateApplicationState
            , IMessenger messenger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _updateApplicationState = updateApplicationState ?? throw new ArgumentNullException(nameof(updateApplicationState));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
        }

        public async Task<Unit> Handle(LoadCharacterRequest request, CancellationToken cancellationToken)
        {
            _messenger.Send(CharacterLoaded.Successful());
            _updateApplicationState.UpdateCharacter(request.Character);

            await _service.NavigateToAsync("//tabbar/character");

            return Unit.Value;
        }
    }
}