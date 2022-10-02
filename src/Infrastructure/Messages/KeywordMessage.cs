using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Messages
{
    public class KeywordMessage
    {
        public bool KeywordsChanged { get; }

        private KeywordMessage(bool keywordsChanged)
        {
            KeywordsChanged = keywordsChanged;
        }

        public static KeywordMessage Changed() => new(true);
    }
}