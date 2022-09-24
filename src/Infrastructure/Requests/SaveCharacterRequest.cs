using MediatR;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class SaveCharacterRequest : IRequest
    {
        private SaveCharacterRequest()
        { }

        public static SaveCharacterRequest Save() => new();
    }
}