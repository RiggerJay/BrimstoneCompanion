using CommunityToolkit.Maui.Core;

namespace RedSpartan.BrimstoneCompanion.MauiUI
{
    internal static class AppRouting
    {
        private static readonly Dictionary<string, (Type Page, Type ViewModel, bool IsPopup)> _mapper = new();

        internal static void RegisterPage<TPage, TViewModel>(this MauiAppBuilder mauiAppBuilder, string route)
            where TPage : ContentPage
            where TViewModel : class
        {
            mauiAppBuilder.Services.AddTransient<TPage>();
            mauiAppBuilder.Services.AddTransient<TViewModel>();

            Routing.RegisterRoute(route, typeof(TPage));

            Register(route, typeof(TPage), typeof(TViewModel), false);
        }

        internal static void RegisterPopup<TPage, TViewModel>(this MauiAppBuilder mauiAppBuilder, string route)
            where TPage : class, IPopup
            where TViewModel : class
        {
            mauiAppBuilder.Services.AddTransient<TPage>();
            mauiAppBuilder.Services.AddTransient<TViewModel>();

            Register(route, typeof(TPage), typeof(TViewModel), false);
        }

        private static void Register(string route, Type page, Type viewModel, bool IsPopup)
        {
            _mapper.Add(route, new(page, viewModel, IsPopup));
        }
    }
}