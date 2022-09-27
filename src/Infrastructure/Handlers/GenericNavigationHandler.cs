using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public abstract class GenericNavigationHandler<TResponse> : IRequestHandler<NavRequest<TResponse>, TResponse>
    {
        private readonly INavigationService _service;
        private readonly IAppRouting _appRouting;

        protected GenericNavigationHandler(INavigationService service
            , IAppRouting appRouting)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _appRouting = appRouting ?? throw new ArgumentNullException(nameof(appRouting));
        }

        public async Task<TResponse> Handle(NavRequest<TResponse> request, CancellationToken cancellationToken)
        {
            if (request.Route == NavigationKeys.BACK)
            {
                _service.Pop(request.Response);
                return request.Response;
            }
            else if (_appRouting.IsPopup(request.Route))
            {
                return await _service.PushAsync<TResponse>(_appRouting.GetPage(request.Route), request.Paramaters);
            }

            throw new InvalidOperationException();
        }
    }
}