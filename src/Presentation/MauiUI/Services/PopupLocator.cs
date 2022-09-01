using CommunityToolkit.Maui.Core;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Services
{
    internal static class PopupLocator
    {
        internal static IServiceProvider ServiceProvider { get; set; }

        internal static T Locate<T>()
        {
            return (T)ServiceProvider.GetService(typeof(T));
        }
    }
}