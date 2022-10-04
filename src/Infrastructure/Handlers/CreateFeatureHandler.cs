using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Infrastructure.Messages;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;
using System;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class CreateFeatureHandler : IRequestHandler<CreateFeatureRequest, bool>
    {
        private readonly IMessenger _messenger;
        private readonly IApplicationState _applicationState;
        private readonly Random _random = new();

        public CreateFeatureHandler(IApplicationState applicationState,
            IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        }

        public Task<bool> Handle(CreateFeatureRequest request, CancellationToken cancellationToken)
        {
            _applicationState.Character.Features.Add(request.Feature);

            _applicationState.Character.CurrentWeight += request.Feature.Weight;

            foreach (var prop in request.Feature.Properties)
            {
                var attribute = _applicationState.Character.Attributes.First(x => x.Key == prop.Key);

                attribute.SetCurrentValues(_applicationState.Character.Features);
                
                if (attribute.Key == AttributeNames.SIDEBAG)
                {
                    SortoutSidebag(attribute);
                }
            }

            _messenger.Send(KeywordMessage.Changed());

            return Task.FromResult(true);
        }

        private void SortoutSidebag(AppLayer.ObservableModels.ObservableAttribute attribute)
        {
            if (_applicationState.Character.Tokens.Count > attribute.CurrentMaxValue)
            {
                var count = _applicationState.Character.Tokens.Count - attribute.CurrentMaxValue;
                while (count != 0)
                {
                    _applicationState.Character.Tokens.RemoveAt(_random.Next(0, _applicationState.Character.Tokens.Count - 1));
                    count--;
                }
            }
        }
    }
}