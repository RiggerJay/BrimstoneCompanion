using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.CQRS
{
    public class NavRequest<T> : IRequest<T>
    {
        internal const string CHARACTER = "Character";
        internal const string ATTRIBUTE = "Attribute";
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
        internal NavRequest(string route, IDictionary<string, object>? paramaters) : base(route, paramaters)
        { }

        internal static NavRequest LoadCharacter(ObservableCharacter character)
            => new(NavigationKeys.CHARACTER, new Dictionary<string, object> { { CHARACTER, character } });

        internal static NavRequest ShowFeatures(ObservableCharacter character)
            => new(NavigationKeys.CHARACTER_FEATURES, new Dictionary<string, object> { { CHARACTER, character } });

        internal static NavRequest ShowNotes(ObservableCharacter character)
            => new(NavigationKeys.CHARACTER_NOTES, new Dictionary<string, object> { { CHARACTER, character } });

        internal static NavRequest Close()
            => new(NavigationKeys.BACK, null);
    }
}