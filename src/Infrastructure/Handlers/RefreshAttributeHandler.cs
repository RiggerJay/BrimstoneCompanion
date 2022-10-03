using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class RefreshAttributeHandler : IRequestHandler<RefreshAttributesRequest, Unit>
    {
        private readonly IApplicationState _applicationState;
        private readonly Random _random = new();

        public RefreshAttributeHandler(IApplicationState applicationState)
        {
            _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        }
        public Task<Unit> Handle(RefreshAttributesRequest request, CancellationToken cancellationToken)
        {
            foreach (var key in request.Keys)
            {
                var attribute = _applicationState.Character.Attributes.First(x => x.Key == key);
                attribute.SetCurrentValues(_applicationState.Character.Features);

                if(attribute.Key == AttributeNames.SIDEBAG)
                {
                    SortoutSidebag(attribute);
                }
            }

            return Unit.Task;
        }


        private void SortoutSidebag(ObservableAttribute sidebag)
        {
            if (_applicationState.Character.Tokens.Count > sidebag.CurrentMaxValue)
            {
                var count = _applicationState.Character.Tokens.Count - sidebag.CurrentMaxValue;
                while (count != 0)
                {
                    _applicationState.Character.Tokens.RemoveAt(_random.Next(0, _applicationState.Character.Tokens.Count - 1));
                    count--;
                }
            }
        }
    }
}
