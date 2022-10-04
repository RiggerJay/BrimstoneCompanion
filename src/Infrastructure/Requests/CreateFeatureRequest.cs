using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class CreateFeatureRequest : IRequest<bool>
    {
        private CreateFeatureRequest(ObservableFeature feature)
        {
            Feature = feature ?? throw new ArgumentNullException(nameof(feature));
        }

        public ObservableFeature Feature { get; }

        public static CreateFeatureRequest With(ObservableFeature feature) => new(feature);
    }
}