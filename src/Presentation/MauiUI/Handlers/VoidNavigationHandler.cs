using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class VoidNavigationHandler : IRequestHandler<NavRequest, Unit>
    {
        private readonly INavigationService _service;

        public VoidNavigationHandler(INavigationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<Unit> Handle(NavRequest request, CancellationToken cancellationToken)
        {
            await _service.NavigateToAsync(request.Route, request.Paramaters);
            return Unit.Value;
        }
    }
}