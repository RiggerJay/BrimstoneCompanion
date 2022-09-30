using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;
using System.Security.Cryptography.X509Certificates;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class UpdateCharacterHandler : IRequestHandler<UpdateCharacterRequest, bool>
    {
        private static bool _updated = false;

        public Task<bool> Handle(UpdateCharacterRequest request, CancellationToken cancellationToken)
        {
            if (request.Template is not null)
            {
                CharacterTemplateUpdate(request.Character, request.Template);
            }

            return Task.FromResult(_updated);
        }

        private static void CharacterTemplateUpdate(ObservableCharacter character, Template template)
        {
            foreach (var attribute in template.Attributes)
            {
                if (!character.Attributes.Any(x => x.Key == attribute.Key))
                {
                    character.Attributes.Add(ObservableAttribute.New(attribute.Key, attribute.Value.Value, attribute.Value.MaxValue, character.Features));
                    _updated = true;
                }
            }
        }
    }
}