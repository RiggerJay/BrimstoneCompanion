using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class StringNavigationHandler : GenericNavigationHandler<string>
    {
        public StringNavigationHandler(INavigationService service) : base(service)
        {
        }
    }
}