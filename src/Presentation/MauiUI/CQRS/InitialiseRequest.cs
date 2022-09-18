using MediatR;

namespace RedSpartan.BrimstoneCompanion.MauiUI.CQRS
{
    public class InitialiseRequest : IRequest
    {
        private InitialiseRequest()
        { }

        public static InitialiseRequest Go() => new();
    }
}