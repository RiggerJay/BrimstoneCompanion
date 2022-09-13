using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class FeaturePage : ContentPage
{
    private bool _busy;
    private readonly ITextResource _textResource;

    public FeaturePage(FeatureViewModel viewModel
        , ITextResource textResource)
    {
        _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
        BindingContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        InitializeComponent();
    }

    public ITextResource TextResource => _textResource;

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        if (_busy == false &&
            sender is VisualElement element)
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
    }
}