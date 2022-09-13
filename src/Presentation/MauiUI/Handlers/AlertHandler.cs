using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class AlertHandler : IRequestHandler<AlertRequest, Unit>
    {
        private readonly IAlertService _service;

        public AlertHandler(IAlertService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<Unit> Handle(AlertRequest request, CancellationToken cancellationToken)
        {
            await _service.DisplayAlert(request.Title, request.Message, request.Cancel);
            return Unit.Value;
        }
    }
}