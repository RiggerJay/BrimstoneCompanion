namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface IAlertService
    {
        Task<bool> DisplayAlert(string title, string body, string accept, string cancel);
    }
}
