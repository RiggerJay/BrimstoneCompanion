using CommunityToolkit.Maui.Views;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface IPopupService
    {
        Task<TResult> PushAsync<TPage, TResult>() where TPage : Popup;

        void Pop<TResult>(TResult result);
    }
}