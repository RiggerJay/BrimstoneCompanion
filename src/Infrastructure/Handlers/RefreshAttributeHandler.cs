using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class RefreshAttributeHandler : IRequestHandler<RefreshAttributesRequest, Unit>
    {
        private readonly IApplicationState _applicationState;
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
            }

            return Unit.Task;
        }
    }
}
