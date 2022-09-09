using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.CQRS
{
    public class SaveCharacterRequest : IRequest
    {
        public ObservableCharacter Character { get; }

        private SaveCharacterRequest(ObservableCharacter character)
        {
            Character = character;
        }

        public static SaveCharacterRequest WithCharacter(ObservableCharacter character)
            => new(character);
    }
}