using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Messages
{
    public class KeywordMessage
    {
        public bool AddedKeyword { get; }

        public ObservableKeyword Keyword { get; }

        private KeywordMessage(ObservableKeyword keyword, bool keywordAdded)
        {
            Keyword = keyword;
            AddedKeyword = keywordAdded;
        }

        public static KeywordMessage Added(ObservableKeyword keyword) => new(keyword, true);
    }
}