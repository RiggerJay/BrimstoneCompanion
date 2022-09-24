using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class LoadCharacterRequest : IRequest
    {
        public ObservableCharacter Character { get; }

        private LoadCharacterRequest(ObservableCharacter character)
        {
            Character = character ?? throw new ArgumentNullException(nameof(character));
        }

        public static LoadCharacterRequest With(ObservableCharacter character) => new(character);
    }
}