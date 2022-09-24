namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface IAppRouting
    {
        Type GetPage(string route);
        bool IsPopup(string route);

        void Register(string route, Type page, Type viewModel, bool IsPopup);
    }
}