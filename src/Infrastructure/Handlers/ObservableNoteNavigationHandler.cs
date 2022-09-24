using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class ObservableNoteNavigationHandler : GenericNavigationHandler<ObservableNote>
    {
        public ObservableNoteNavigationHandler(INavigationService service, IAppRouting appRouting) : base(service, appRouting)
        {
        }
    }
}