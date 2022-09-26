using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class LevelUpCharacterRequest : IRequest<bool>
    {
        private LevelUpCharacterRequest(ObservableAttribute attribute, int value)
        {
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
            Value = value;
        }

        public ObservableAttribute Attribute { get; }

        public int Value { get; }

        public static LevelUpCharacterRequest With(ObservableAttribute attribute, int value) => new(attribute, value);
    }
}