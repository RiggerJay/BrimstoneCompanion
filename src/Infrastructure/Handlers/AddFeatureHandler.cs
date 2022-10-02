using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Infrastructure.Messages;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;
using RedSpartan.BrimstoneCompanion.Infrastructure.Services;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class AddFeatureHandler : IRequestHandler<AddFeatureRequest, bool>
    {
        private readonly IMessenger _messenger;
        private readonly IApplicationState _applicationState;

        public AddFeatureHandler(IApplicationState applicationState,
            IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        }

        public Task<bool> Handle(AddFeatureRequest request, CancellationToken cancellationToken)
        {
            _applicationState.Character.Features.Add(request.Feature);

            _applicationState.Character.CurrentWeight += request.Feature.Weight;

            foreach (var prop in request.Feature.Properties)
            {
                var attribute = _applicationState.Character.Attributes.First(x => x.Key == prop.Key);

                attribute.SetCurrentValues(_applicationState.Character.Features);

                if(attribute.Key == AttributeNames.SIDEBAG
                    && _applicationState.Character.Tokens.Count > attribute.CurrentMaxValue)
                {
                    var count = _applicationState.Character.Tokens.Count - attribute.CurrentMaxValue;
                    while (count != 0)
                    {
                        _applicationState.Character.Tokens.RemoveAt(_applicationState.Character.Tokens.Count - 1);
                        count--;
                    }
                }
            }

            _messenger.Send(KeywordMessage.Changed());

            return Task.FromResult(true);
        }
    }
}