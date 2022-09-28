using MediatR;
using Newtonsoft.Json.Linq;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
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
            var attribute = request.Attribute;

            if (!attribute.MaxValue.HasValue)
            {
                attribute.CurrentValue = attribute.GetCurrentValue(request.Value, _applicationState.Character);
                attribute.Value = request.Value;
                attribute.MaxValue = null;
                attribute.CurrentMaxValue = null;
            }
            else
            {
                attribute.Value = GetValue(attribute.MaxValue.Value, request.Value);
            }

            return Task.FromResult(true);
        }

        private static int GetValue(int maxValue, int value)
        {
            if (maxValue > value)
            {
                return value;
            }
            
            return maxValue;
        }
    }
}