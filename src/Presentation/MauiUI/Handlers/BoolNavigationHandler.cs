using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class BoolNavigationHandler : GenericNavigationHandler<bool>
    {
        public BoolNavigationHandler(INavigationService service) : base(service)
        {
        }
    }
}