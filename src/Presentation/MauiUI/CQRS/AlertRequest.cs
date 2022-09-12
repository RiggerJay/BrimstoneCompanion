using MediatR;

namespace RedSpartan.BrimstoneCompanion.MauiUI.CQRS
{
    public class AlertRequest : IRequest<bool>
    {
        public string Title { get; }
        public string Message { get; }
        public string Accept { get; }
        public string Cancel { get; }

        private AlertRequest(string title, string message, string accept = null, string cancel = null)
        {
            Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentNullException(nameof(title)) : title;
            Message = string.IsNullOrWhiteSpace(message) ? throw new ArgumentNullException(nameof(message)) : message;
            Accept = string.IsNullOrWhiteSpace(accept) ? "Yes" : accept;
            Cancel = string.IsNullOrWhiteSpace(cancel) ? "No" : cancel;
        }

        public static AlertRequest WithTitleAndMessage(string title, string message) => new(title, message);
    }
}