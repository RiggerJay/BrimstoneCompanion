using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.CQRS
{
    public class NavRequest<T> : IRequest<T>
    {
        internal const string CHARACTER = "Character";
        internal const string ATTRIBUTE = "Attribute";
        internal const string NOTE = "Note";
        internal const string FEATURE = "Feature";

        internal string Route { get; }

        internal IDictionary<string, object>? Paramaters { get; }

        internal T? Response { get; }

        protected NavRequest(string route, IDictionary<string, object>? paramaters = null, T? response = default)
        {
            Route = string.IsNullOrWhiteSpace(route) ? throw new ArgumentNullException(nameof(route)) : route;
            Paramaters = paramaters;
            Response = response;
        }

        internal static NavRequest<ObservableCharacter> CreateCharacter()
            => new(NavigationKeys.CHARACTER_CREATE, null);

        internal static NavRequest<ObservableFeature> CreateFeature()
            => new(NavigationKeys.FEATURE_CREATE, null);

        internal static NavRequest<ObservableNote> CreateNote()
            => new(NavigationKeys.NOTE_CREATE, null);

        internal static NavRequest<string> Keyword()
            => new(NavigationKeys.KEYWORD, null);

        internal static NavRequest<bool> EditNote(ObservableNote note)
            => new(NavigationKeys.NOTE_EDIT, new Dictionary<string, object> { { NOTE, note } });

        internal static NavRequest<bool> EditFeature(ObservableFeature feature)
            => new(NavigationKeys.FEATURE_EDIT, new Dictionary<string, object> { { FEATURE, feature } });

        internal static NavRequest<bool> UpdateAttribute(ObservableAttribute attribute)
            => new(NavigationKeys.ATTRIBUTE_UPDATE, new Dictionary<string, object> { { ATTRIBUTE, attribute } });

        internal static NavRequest<bool> IncrementAttribute(ObservableAttribute attribute)
            => new(NavigationKeys.ATTRIBUTE_INCREMENT, new Dictionary<string, object> { { ATTRIBUTE, attribute } });

        internal static NavRequest<TResponse> Close<TResponse>(TResponse result)
            => new(NavigationKeys.BACK, null, result);

        internal static NavRequest<bool> LevelUp(ObservableCharacter character)
            => new(NavigationKeys.CHARACTER_LEVELUP, new Dictionary<string, object> { { CHARACTER, character } });
    }

    public class NavRequest : NavRequest<Unit>
    {
        internal NavRequest(string route, IDictionary<string, object>? paramaters = null) : base(route, paramaters)
        { }

        internal static NavRequest ShowCharacter()
            => new($"//tabbar/{NavigationKeys.CHARACTER}");

        internal static NavRequest ShowFeatures()
            => new($"//tabbar/{NavigationKeys.CHARACTER_FEATURES}");

        internal static NavRequest ShowNotes()
            => new($"//tabbar/{NavigationKeys.CHARACTER_NOTES}");

        internal static NavRequest CharacterSelector()
            => new($"//tabbar/{NavigationKeys.CHARACTER_SELECTOR}");

        internal static NavRequest Close()
            => new(NavigationKeys.BACK, null);
    }
}