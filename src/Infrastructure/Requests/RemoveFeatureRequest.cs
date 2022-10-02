using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class RemoveFeatureRequest : IRequest
    {
        private RemoveFeatureRequest(ObservableFeature feature)
        {
            Feature = feature ?? throw new ArgumentNullException(nameof(feature));
        }

        public ObservableFeature Feature { get; }

        public static RemoveFeatureRequest With(ObservableFeature feature) => new(feature);
    }
}