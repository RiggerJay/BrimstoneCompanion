using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class NavRequest<T> : IRequest<T>
    {
        protected const string CHARACTER = "Character";
        protected const string ATTRIBUTE = "Attribute";
        protected const string NOTE = "Note";
        protected const string FEATURE = "Feature";

        internal string Route { get; }

        internal IDictionary<string, object>? Paramaters { get; }

        internal T? Response { get; }

        protected NavRequest(string route, IDictionary<string, object>? paramaters = null, T? response = default)
        {
            Route = string.IsNullOrWhiteSpace(route) ? throw new ArgumentNullException(nameof(route)) : route;
            Paramaters = paramaters;
            Response = response;
        }

        public static NavRequest<ObservableCharacter> CreateCharacter()
            => new(NavigationKeys.CHARACTER_CREATE, null);

        public static NavRequest<ObservableCharacter> ImportCharacter()
            => new(NavigationKeys.CHARACTER_IMPORT, null);

        public static NavRequest<ObservableNote> CreateNote()
            => new(NavigationKeys.NOTE_CREATE, null);

        public static NavRequest<string> Keyword()
            => new(NavigationKeys.KEYWORD, null);

        public static NavRequest<bool> EditNote(ObservableNote note)
            => new(NavigationKeys.NOTE_EDIT, new Dictionary<string, object> { { NOTE, note } });

        public static NavRequest<bool> UpdateAttribute(ObservableAttribute attribute)
            => new(NavigationKeys.ATTRIBUTE_UPDATE, new Dictionary<string, object> { { ATTRIBUTE, attribute } });

        public static NavRequest<bool> IncrementAttribute(ObservableAttribute attribute)
            => new(NavigationKeys.ATTRIBUTE_INCREMENT, new Dictionary<string, object> { { ATTRIBUTE, attribute } });

        public static NavRequest<TResponse> Close<TResponse>(TResponse result)
            => new(NavigationKeys.BACK, null, result);

        public static NavRequest<bool> ShowSidebag()
            => new(NavigationKeys.CHARACTER_SIDEBAG, null);

        public static NavRequest<bool> LevelUp(ObservableCharacter character)
            => new(NavigationKeys.CHARACTER_LEVELUP, new Dictionary<string, object> { { CHARACTER, character } });

        public static NavRequest<bool> ExportCharacter(ObservableCharacter character)
            => new(NavigationKeys.CHARACTER_EXPORT, new Dictionary<string, object> { { CHARACTER, character } });
    }

    public class NavRequest : NavRequest<Unit>
    {
        private NavRequest(string route, IDictionary<string, object>? paramaters = null) : base(route, paramaters)
        { }

        public static NavRequest ShowCharacter()
            => new($"//tabbar/{NavigationKeys.CHARACTER}");

        public static NavRequest ShowFeatures()
            => new($"//tabbar/{NavigationKeys.CHARACTER_FEATURES}");

        public static NavRequest ShowNotes()
            => new($"//tabbar/{NavigationKeys.CHARACTER_NOTES}");

        public static NavRequest CharacterSelector()
            => new($"//tabbar/{NavigationKeys.CHARACTER_SELECTOR}");

        public static NavRequest Close()
            => new(NavigationKeys.BACK, null);

        public static NavRequest EditFeature(ObservableFeature feature)
            => new(NavigationKeys.FEATURE_EDIT, new Dictionary<string, object> { { FEATURE, feature } });

        public static NavRequest CreateFeature()
            => new(NavigationKeys.FEATURE_CREATE, null);
    }
}