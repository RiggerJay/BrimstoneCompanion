using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class CreateCharacterHandler : IRequestHandler<CreateCharacterRequest, ObservableCharacter>
    {
        private readonly ICharacterService _service;

        public CreateCharacterHandler(ICharacterService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<ObservableCharacter> Handle(CreateCharacterRequest request, CancellationToken cancellationToken)
        {
            return await _service.NewAsync(request.Name, request.Role);
        }
    }
}