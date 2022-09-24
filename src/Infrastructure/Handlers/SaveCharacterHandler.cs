using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class SaveCharacterHandler : IRequestHandler<SaveCharacterRequest, Unit>
    {
        private readonly ICharacterService _service;
        private readonly IApplicationState _state;

        public SaveCharacterHandler(ICharacterService service, IApplicationState state)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public async Task<Unit> Handle(SaveCharacterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.SaveAsync(_state.Character);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Unit.Value;
        }
    }
}