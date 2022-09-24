using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class InitialiseHandler : IRequestHandler<InitialiseRequest, Unit>
    {
        private readonly ICharacterService _service;

        public InitialiseHandler(ICharacterService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<Unit> Handle(InitialiseRequest request, CancellationToken cancellationToken)
        {
            await _service.Initialise();
            return Unit.Value;
        }
    }
}