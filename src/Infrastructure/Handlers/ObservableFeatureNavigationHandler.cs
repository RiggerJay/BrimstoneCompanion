using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class ObservableFeatureNavigationHandler : GenericNavigationHandler<ObservableFeature>
    {
        public ObservableFeatureNavigationHandler(INavigationService service, IAppRouting appRouting) : base(service, appRouting)
        {
        }
    }
}