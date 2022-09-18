using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using RedSpartan.BrimstoneCompanion.MauiUI.Utilities;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class FeaturePage : ContentPage
{
    private readonly ITextResource _textResource;
    private readonly IMediator _mediator;
    private readonly FeatureViewModel _viewModel;

    public FeaturePage(IMediator mediator
        , FeatureViewModel viewModel
        , ITextResource textResource)
    {
        _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        if (!_viewModel.CharacterLoaded)
        {
            Task.Run(() => _mediator.Send(NavRequest.CharacterSelector()));
        }
        base.OnAppearing();
    }

    public ITextResource TextResource => _textResource;

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await sender.Bounce();
    }
}