using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class BoolNavigationHandler : GenericNavigationHandler<bool>
    {
        public BoolNavigationHandler(INavigationService service, IAppRouting appRouting) : base(service, appRouting)
        {
        }
    }
}