using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using CommunityToolkit.Maui.Views;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Services
{
    internal class MauiNavigationService : INavigationService
    {
        public Task InitializeAsync()
        {
            return NavigateToAsync("//MainPage");
        }

        public Task NavigateBackAsync()
        {
            return Shell.Current.GoToAsync("..");
        }

        public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
        {
            return routeParameters != null
                ? Shell.Current.GoToAsync(route, routeParameters)
                : Shell.Current.GoToAsync(route);
        }

        private Popup? _currentPopup;

        public void Pop<TResult>(TResult result)
        {
            _currentPopup?.Close(result);
        }

        public async Task<TResult> PushAsync<TPage, TResult>() where TPage : Popup
        {
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(_currentPopup = PopupLocator.Locate<TPage>());

            if (result is TResult model)
            {
                return model;
            }
            return default;
        }
    }
}