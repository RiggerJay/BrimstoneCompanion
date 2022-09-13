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

        public async Task<TResult> PushAsync<TResult>(Type pageType, IDictionary<string, object> data = null)
        {
            var page = (Popup)PopupLocator.Locate(pageType);

            if (data != null
                && page is IInitialisePopup init)
            {
                init.Initialise(data);
            }
            var result = await Shell.Current.CurrentPage.ShowPopupAsync(_currentPopup = page);

            if (result is TResult model)
            {
                return model;
            }
            return default;
        }

        public Task<bool> DisplayAlert(string title, string body, string accept, string cancel)
        {
            return Shell.Current.DisplayAlert(title, body, accept, cancel);
        }

        public Task DisplayAlert(string title, string body, string cancel)
        {
            return Shell.Current.DisplayAlert(title, body, cancel);
        }
    }
}