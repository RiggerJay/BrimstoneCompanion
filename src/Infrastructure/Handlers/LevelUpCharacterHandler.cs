using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class LevelUpCharacterHandler : IRequestHandler<LevelUpCharacterRequest, bool>
    {
        private readonly INavigationService _service;

        public LevelUpCharacterHandler(INavigationService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public Task<bool> Handle(LevelUpCharacterRequest request, CancellationToken cancellationToken)
        {
            //TODO: Level up the character
            return Task.FromResult(true);
        }
    }
}