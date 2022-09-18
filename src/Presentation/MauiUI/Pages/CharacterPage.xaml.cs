using MediatR;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class CharacterPage : ContentPage
{
    private readonly IMediator _mediator;
    private readonly CharacterViewModel _viewModel;

    public CharacterPage(IMediator mediator
        , CharacterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    protected override void OnAppearing()
    {
        if (!_viewModel.CharacterLoaded)
        {
            Task.Run(() => _mediator.Send(NavRequest.CharacterSelector()));
        }
        base.OnAppearing();
    }
}