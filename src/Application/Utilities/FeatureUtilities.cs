using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Utilities
{
    internal static class FeatureUtilities
    {
        internal static int GetFeatureCount(this IList<ObservableFeature> features, string key) =>
            features.SelectMany(x => x.Properties.Where(prop => prop.Key == key).Select(prop => prop.Value)).Sum();
    }
}