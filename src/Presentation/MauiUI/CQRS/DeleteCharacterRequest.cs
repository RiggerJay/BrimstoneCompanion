using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.CQRS
{
    public class DeleteCharacterRequest : IRequest<bool>
    {
        public ObservableCharacter Character { get; }

        private DeleteCharacterRequest(ObservableCharacter character)
        {
            Character = character;
        }

        public static DeleteCharacterRequest WithCharacter(ObservableCharacter character)
            => new(character);
    }
}