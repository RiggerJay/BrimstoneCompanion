using MediatR;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class InitialiseRequest : IRequest
    {
        private InitialiseRequest()
        { }

        public static InitialiseRequest Go() => new();
    }
}