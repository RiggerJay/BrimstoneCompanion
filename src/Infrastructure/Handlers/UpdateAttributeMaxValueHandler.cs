using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class UpdateAttributeMaxValueHandler : IRequestHandler<UpdateAttributeMaxValueRequest, Unit>
    {
        private readonly IApplicationState _applicationState;

        public UpdateAttributeMaxValueHandler(IApplicationState applicationState)
        {
            _applicationState = applicationState ?? throw new ArgumentNullException(nameof(applicationState));
        }

        public Task<Unit> Handle(UpdateAttributeMaxValueRequest request, CancellationToken cancellationToken)
        {
            request.Attribute.SetMaxValue(request.MaxValue, _applicationState.Character.Features);

            return Unit.Task;
        }
    }
}