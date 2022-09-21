using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class EditFeaturePage : ContentPage
{
    private readonly ITextResource _textResource;

    public EditFeaturePage(EditFeatureViewModel viewModel, ITextResource textResource)
    {
        _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
        BindingContext = viewModel;
        InitializeComponent();
    }

    public ITextResource TextResource => _textResource;
}