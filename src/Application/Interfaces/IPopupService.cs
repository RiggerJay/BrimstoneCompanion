
namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface IPopupService
    {
        Task<TResult> PushAsync<TResult>(Type pageType, IDictionary<string, object> data = null);

        void Pop<TResult>(TResult result);
    }
}