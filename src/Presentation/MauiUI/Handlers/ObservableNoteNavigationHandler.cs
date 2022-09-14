using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class ObservableNoteNavigationHandler : GenericNavigationHandler<ObservableNote>
    {
        public ObservableNoteNavigationHandler(INavigationService service) : base(service)
        {
        }
    }
}