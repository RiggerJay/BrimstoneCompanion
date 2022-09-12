using CommunityToolkit.Maui.Views;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface IPopupService
    {
        Task<TResult> PushAsync<TPage, TResult>() where TPage : Popup;

        Task<TResult> PushAsync<TResult>(Type pageType);

        Task<TResult> PushAsync<TPage, TResult>(IDictionary<string, object> data) where TPage : Popup, IInitialisePopup;

        void Pop<TResult>(TResult result);
    }
}