using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class CreateCharacterRequest : IRequest<ObservableCharacter>
    {
        public string Name { get; }
        public string Role { get; }

        private CreateCharacterRequest(string name, string role)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
            Role = string.IsNullOrWhiteSpace(role) ? throw new ArgumentNullException(nameof(role)) : role;
        }

        public static CreateCharacterRequest With(string name, string role) => new(name, role);
    }
}