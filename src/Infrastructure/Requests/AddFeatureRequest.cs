using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class AddFeatureRequest : IRequest<bool>
    {
        private AddFeatureRequest(ObservableFeature feature)
        {
            Feature = feature ?? throw new ArgumentNullException(nameof(feature));
        }

        public ObservableFeature Feature { get; }

        public static AddFeatureRequest With(ObservableFeature feature) => new(feature);
    }
}