using MediatR;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using RedSpartan.BrimstoneCompanion.MauiUI.Utilities;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class NotesPage : ContentPage
{
    private readonly IMediator _mediator;
    private readonly NotesViewModel _viewModel;

    public NotesPage(IMediator mediator
        , NotesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    protected override async void OnAppearing()
    {
        if (!_viewModel.CharacterLoaded)
        {
            await _mediator.Send(NavRequest.CharacterSelector());
        }
        base.OnAppearing();
    }

    private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        await sender.Bounce();
    }
}