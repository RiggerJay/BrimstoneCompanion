using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class ObservableFeatureNavigationHandler : GenericNavigationHandler<ObservableFeature>
    {
        public ObservableFeatureNavigationHandler(INavigationService service) : base(service)
        {
        }
    }
}