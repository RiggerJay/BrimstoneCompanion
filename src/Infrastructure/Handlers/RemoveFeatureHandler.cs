using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Messages;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class RemoveFeatureHandler : IRequestHandler<RemoveFeatureRequest, Unit>
    {
        private readonly IMessenger _messenger;
        private readonly IApplicationState _applicationState;

        public RemoveFeatureHandler(IApplicationState applicationState,
            IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        }

        public Task<Unit> Handle(RemoveFeatureRequest request, CancellationToken cancellationToken)
        {
            _applicationState.Character.Features.Remove(request.Feature);

            _applicationState.Character.CurrentWeight -= request.Feature.Weight;

            foreach (var prop in request.Feature.Properties)
            {
                var attribute = _applicationState.Character.Attributes.First(x => x.Key == prop.Key);

                attribute.SetCurrentValues(_applicationState.Character.Features);
            }

            _messenger.Send(KeywordMessage.Changed());

            return Unit.Task;
        }
    }
}