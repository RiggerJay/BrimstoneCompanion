using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class ObservableCharacterNavigationHandler : GenericNavigationHandler<ObservableCharacter>
    {
        public ObservableCharacterNavigationHandler(INavigationService service, IAppRouting appRouting) : base(service, appRouting)
        {
        }
    }
}