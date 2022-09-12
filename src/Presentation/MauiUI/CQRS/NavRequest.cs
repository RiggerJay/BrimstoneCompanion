using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.CQRS
{
    public class NavRequest<T> : IRequest<T>
    {
        public string Route { get; }

        public IDictionary<string, object>? Paramaters { get; }

        protected NavRequest(string route, IDictionary<string, object>? paramaters)
        {
            Route = string.IsNullOrWhiteSpace(route) ? throw new ArgumentNullException(nameof(route)) : route;
            Paramaters = paramaters;
        }

        internal static NavRequest<ObservableCharacter> CreateCharacter()
            => new(NavigationKeys.CHARACTER_CREATE, null);

        internal static NavRequest<ObservableFeature> CreateFeature()
            => new(NavigationKeys.FEATURE_CREATE, null);
    }

    public class NavRequest : NavRequest<Unit>
    {
        private NavRequest(string route, IDictionary<string, object>? paramaters) : base(route, paramaters)
        { }

        public static NavRequest LoadCharacter(ObservableCharacter character)
            => new(NavigationKeys.CHARACTER, new Dictionary<string, object> { { "Character", character } });
    }
}