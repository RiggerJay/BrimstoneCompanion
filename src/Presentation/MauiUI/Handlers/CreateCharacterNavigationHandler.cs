using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class CreateCharacterNavigationHandler : GenericNavigationHandler<ObservableCharacter>
    {
        public CreateCharacterNavigationHandler(INavigationService service) : base(service)
        {
        }
    }
}