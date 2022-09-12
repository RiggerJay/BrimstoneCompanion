using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class ObservableCharacterNavigationHandler : GenericNavigationHandler<ObservableCharacter>
    {
        public ObservableCharacterNavigationHandler(INavigationService service) : base(service)
        {
        }
    }
}