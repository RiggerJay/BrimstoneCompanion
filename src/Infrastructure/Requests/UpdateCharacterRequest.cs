using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class UpdateCharacterRequest : IRequest<bool>
    {
        private UpdateCharacterRequest(ObservableCharacter character)
        {
            Character = character ?? throw new ArgumentNullException(nameof(character));
        }

        private UpdateCharacterRequest(ObservableCharacter character, Template template) : this(character)
        {
            Character = character ?? throw new ArgumentNullException(nameof(character));
            Template = template ?? throw new ArgumentNullException(nameof(template));
        }

        public ObservableCharacter Character { get; }

        public Template? Template { get; }

        public static UpdateCharacterRequest With(ObservableCharacter character) => new(character);

        public static UpdateCharacterRequest With(ObservableCharacter character, Template template) => new(character, template);
    }
}