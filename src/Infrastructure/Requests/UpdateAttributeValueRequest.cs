using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class UpdateAttributeValueRequest : IRequest<bool>
    {
        private UpdateAttributeValueRequest(ObservableAttribute attribute, int value, int? maxValue)
        {
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            Value = value;
            MaxValue = maxValue;
        }

        public ObservableAttribute Attribute { get; }

        public int Value { get; }

        public int? MaxValue { get; }

        public static UpdateAttributeValueRequest With(ObservableAttribute attribute, int value, int? maxValue) => new(attribute, value, maxValue);
    }
}