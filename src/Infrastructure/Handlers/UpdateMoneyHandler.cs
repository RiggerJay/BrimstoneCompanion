using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class UpdateMoneyHandler : IRequestHandler<UpdateMoneyRequest, Unit>
    {
        private readonly IApplicationState _state;

        public UpdateMoneyHandler(IApplicationState state)
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public Task<Unit> Handle(UpdateMoneyRequest request, CancellationToken cancellationToken)
        {
            var dollars = _state.Character.Attributes.First(x => x.Key == AttributeNames.DOLLARS);
            dollars.SetValue(dollars.Value + request.Value, _state.Character.Features);

            return Unit.Task;
        }
    }
}
