using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Messages
{
    public class RemoveFeature
    {
        public ObservableFeature Feature { get; }

        private RemoveFeature(ObservableFeature feature)
        {
            Feature = feature ?? throw new ArgumentNullException(nameof(feature));
        }

        public static RemoveFeature With(ObservableFeature feature) => new(feature);
    }
}