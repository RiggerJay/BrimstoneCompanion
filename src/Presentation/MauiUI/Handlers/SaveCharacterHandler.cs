using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class SaveCharacterHandler : IRequestHandler<SaveCharacterRequest>
    {
        private readonly ICharacterService _service;

        public SaveCharacterHandler(ICharacterService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<Unit> Handle(SaveCharacterRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.SaveAsync(request.Character);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Unit.Value;
        }
    }
}