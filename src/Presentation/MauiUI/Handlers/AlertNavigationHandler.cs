using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class AlertNavigationHandler : IRequestHandler<AlertRequest, bool>
    {
        private readonly INavigationService _service;

        public AlertNavigationHandler(INavigationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<bool> Handle(AlertRequest request, CancellationToken cancellationToken)
        {
            return await _service.DisplayAlert(request.Title, request.Message, request.Accept, request.Cancel);
        }
    }
}