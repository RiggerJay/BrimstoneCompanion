using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class CreateFeatureNavigationHandler : GenericNavigationHandler<ObservableFeature>
    {
        public CreateFeatureNavigationHandler(INavigationService service) : base(service)
        {
        }
    }
}