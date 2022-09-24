using MediatR;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class BoolAlertRequest : IRequest<bool>
    {
        public string Title { get; }
        public string Message { get; }
        public string Accept { get; }
        public string Cancel { get; }

        private BoolAlertRequest(string title, string message, string accept = "Yes", string cancel = "No")
        {
            Title = string.IsNullOrWhiteSpace(title) ? throw new ArgumentNullException(nameof(title)) : title;
            Message = string.IsNullOrWhiteSpace(message) ? throw new ArgumentNullException(nameof(message)) : message;
            Cancel = string.IsNullOrWhiteSpace(cancel) ? throw new ArgumentNullException(nameof(cancel)) : cancel;
            Accept = string.IsNullOrWhiteSpace(accept) ? throw new ArgumentNullException(nameof(accept)) : accept;
        }

        public static BoolAlertRequest WithTitleAndMessage(string title, string message) => new(title, message);
    }
}