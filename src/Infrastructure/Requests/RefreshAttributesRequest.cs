using MediatR;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class RefreshAttributesRequest : IRequest
    {
        private RefreshAttributesRequest(IList<string> keys)
        {
            Keys = keys ?? throw new ArgumentNullException(nameof(keys));
        }

        public IList<string> Keys { get; }

        public static RefreshAttributesRequest With(IList<string> keys) => new(keys);
    }
}