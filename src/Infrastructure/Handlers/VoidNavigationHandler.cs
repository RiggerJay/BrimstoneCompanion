using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
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
            if (request.Route == NavigationKeys.BACK)
            {
                await _service.NavigateBackAsync();
            }
            else
            {
                await _service.NavigateToAsync(request.Route, request.Paramaters);
            }
            return Unit.Value;
        }
    }
}