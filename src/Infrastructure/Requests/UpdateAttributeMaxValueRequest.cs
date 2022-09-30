using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class UpdateAttributeMaxValueRequest : IRequest
    {
        private UpdateAttributeMaxValueRequest(ObservableAttribute attribute, int maxValue)
        {
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            MaxValue = maxValue;
        }

        public ObservableAttribute Attribute { get; }

        public int MaxValue { get; }

        public static UpdateAttributeMaxValueRequest With(ObservableAttribute attribute, int maxValue) => new(attribute, maxValue);
    }
}