using MediatR;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class AlertRequest : IRequest
    {
        public string Title { get; }
        public string Message { get; }
        public string Cancel { get; }

        private AlertRequest(string title, string message, string cancel)
        {
            Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentNullException(nameof(title)) : title;
            Message = string.IsNullOrWhiteSpace(message) ? throw new ArgumentNullException(nameof(message)) : message;
            Cancel = string.IsNullOrWhiteSpace(cancel) ? throw new ArgumentNullException(nameof(cancel)) : cancel;
        }

        public static AlertRequest WithTitleAndMessage(string title, string message, string cancel = "Ok")
            => new(title, message, cancel);
    }
}