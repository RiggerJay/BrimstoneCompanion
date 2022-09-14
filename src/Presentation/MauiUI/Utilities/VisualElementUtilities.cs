namespace RedSpartan.BrimstoneCompanion.MauiUI.Utilities
{
    internal static class VisualElementUtilities
    {
        private static bool _busy = false;

        internal static bool CanBounce(this VisualElement element) => !_busy;

        internal static async Task Bounce(this VisualElement element)
        {
            _busy = true;
            await Task.WhenAll(
                element.ScaleXTo(.5, 100),
                element.ScaleTo(.5, 100));
            await Task.WhenAll(
                element.ScaleXTo(1, 50),
                element.ScaleTo(1, 50));
            _busy = false;
        }

        internal static Task Bounce(this object obj)
        {
            if (obj is VisualElement element)
            {
                if (element.CanBounce())
                {
                    return element.Bounce();
                }
            }
            return Task.CompletedTask;
        }
    }
}