using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class DeleteCharacterHandler : IRequestHandler<DeleteCharacterRequest, bool>
    {
        private readonly ICharacterService _service;

        public DeleteCharacterHandler(ICharacterService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<bool> Handle(DeleteCharacterRequest request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                success = await _service.DeleteAsync(request.Character);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return success;
        }
    }
}