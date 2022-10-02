using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class LevelUpCharacterHandler : IRequestHandler<LevelUpCharacterRequest, bool>
    {
        private readonly IApplicationState _state;

        public LevelUpCharacterHandler(IApplicationState state)
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public Task<bool> Handle(LevelUpCharacterRequest request, CancellationToken cancellationToken)
        {
            request.Attribute.SetValue(request.Value, _state.Character.Features);
            _state.Character.Level += 1;
            _state.Character.Notes.Add(ObservableNote.New($"{_state.Character.Class} Level {_state.Character.Level}"));
            return Task.FromResult(true);
        }
    }
}