using MediatR;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class UpdateMoneyRequest : IRequest
    {
        public int Value { get; }

        private UpdateMoneyRequest(int value)
        {
            Value = value;
        }

        public static UpdateMoneyRequest WithValue(int value) => new(value);
    }
}