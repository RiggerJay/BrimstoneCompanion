using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class GenericNavigationHandler<TResponse> : IRequestHandler<NavRequest<TResponse>, TResponse>
    {
        private readonly INavigationService _service;

        public GenericNavigationHandler(INavigationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<TResponse> Handle(NavRequest<TResponse> request, CancellationToken cancellationToken)
        {
            if (AppRouting.IsPopup(request.Route))
            {
                return await _service.PushAsync<TResponse>(AppRouting.GetPage(request.Route));
            }

            //TODO: use specific Exception
            throw new NotImplementedException();
        }
    }
}