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

        public Task<bool> Handle(DeleteCharacterRequest request, CancellationToken cancellationToken)
        {
            var success = false;
            try
            {
                success = _service.Delete(request.Character);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.FromResult(success);
        }
    }
}