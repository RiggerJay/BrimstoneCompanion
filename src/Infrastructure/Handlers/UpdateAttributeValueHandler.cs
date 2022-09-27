using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;
using RedSpartan.BrimstoneCompanion.Infrastructure.Utilities;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class UpdateAttributeValueHandler : IRequestHandler<UpdateAttributeValueRequest, bool>
    {
        private readonly IApplicationState _applicationState;

        public UpdateAttributeValueHandler(IApplicationState applicationState)
        {
            _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        }

        public Task<bool> Handle(UpdateAttributeValueRequest request, CancellationToken cancellationToken)
        {
            request.Attribute.SetValue(request.Value, request.Attribute.GetCurrentValue(request.Value, _applicationState.Character));

            return Task.FromResult(true);
        }
    }
}