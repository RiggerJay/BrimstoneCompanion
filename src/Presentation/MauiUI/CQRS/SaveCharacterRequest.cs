using MediatR;

namespace RedSpartan.BrimstoneCompanion.MauiUI.CQRS
{
    public class SaveCharacterRequest : IRequest
    {
        private SaveCharacterRequest()
        { }

        public static SaveCharacterRequest Save() => new();
    }
}