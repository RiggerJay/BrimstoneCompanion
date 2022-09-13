using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Handlers
{
    public class SaveCharacterHandler : IRequestHandler<SaveRequest<ObservableCharacter>, ObservableCharacter>
    {
        private readonly ICharacterService _service;

        public SaveCharacterHandler(ICharacterService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<ObservableCharacter> Handle(SaveRequest<ObservableCharacter> request, CancellationToken cancellationToken)
        {
            try
            {
                await _service.SaveAsync(request.Model);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return request.Model;
        }
    }
}