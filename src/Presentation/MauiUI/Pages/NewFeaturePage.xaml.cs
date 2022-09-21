using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class NewFeaturePage : ContentPage
{
    private readonly ITextResource _textResource;

    public NewFeaturePage(NewFeatureViewModel viewModel, ITextResource textResource)
    {
        _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
        BindingContext = viewModel;
        InitializeComponent();
    }

    public ITextResource TextResource => _textResource;
}