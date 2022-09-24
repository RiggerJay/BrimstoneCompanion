using CommunityToolkit.Maui.Core;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;

namespace RedSpartan.BrimstoneCompanion.MauiUI
{
    public sealed class AppRouting : IAppRouting
    {
        private static readonly Lazy<IAppRouting> lazy =
            new(() => new AppRouting());

        public static IAppRouting Current { get { return lazy.Value; } }

        private AppRouting()
        {
        }

        private readonly Dictionary<string, (Type Page, Type ViewModel, bool IsPopup)> _mapper = new();

        public Type GetPage(string route)
        {
            return _mapper[route].Page;
        }

        public bool IsPopup(string route)
        {
            return _mapper[route].IsPopup;
        }

        public void Register(string route, Type page, Type viewModel, bool IsPopup)
        {
            _mapper.Add(route, new(page, viewModel, IsPopup));
        }
    }

    public static class AppRoutingRegister
    {
        internal static void RegisterPage<TPage, TViewModel>(this MauiAppBuilder mauiAppBuilder, string route)
            where TPage : ContentPage
            where TViewModel : class
        {
            mauiAppBuilder.Services.AddTransient<TPage>();
            mauiAppBuilder.Services.AddTransient<TViewModel>();

            Routing.RegisterRoute(route, typeof(TPage));

            AppRouting.Current.Register(route, typeof(TPage), typeof(TViewModel), false);
        }

        internal static void RegisterPopup<TPage, TViewModel>(this MauiAppBuilder mauiAppBuilder, string route)
            where TPage : class, IPopup
            where TViewModel : class
        {
            mauiAppBuilder.Services.AddTransient<TPage>();
            mauiAppBuilder.Services.AddTransient<TViewModel>();

            AppRouting.Current.Register(route, typeof(TPage), typeof(TViewModel), true);
        }
    }
}