namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface INavigationService : IPopupService
    {
        Task InitializeAsync();

        Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null);

        Task NavigateBackAsync();
    }
}