using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class BoolAlertHandler : IRequestHandler<BoolAlertRequest, bool>
    {
        private readonly IAlertService _service;

        public BoolAlertHandler(IAlertService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<bool> Handle(BoolAlertRequest request, CancellationToken cancellationToken)
        {
            return await _service.DisplayAlert(request.Title, request.Message, request.Accept, request.Cancel);
        }
    }
}