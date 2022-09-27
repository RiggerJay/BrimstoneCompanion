using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class StringNavigationHandler : GenericNavigationHandler<string>
    {
        public StringNavigationHandler(INavigationService service, IAppRouting appRouting)
            : base(service, appRouting)
        {
        }
    }
}