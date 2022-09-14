using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class EditNotePopup : CommunityToolkit.Maui.Views.Popup, IInitialisePopup
{
    private readonly EditNoteViewModel _viewModel;

    public EditNotePopup(EditNoteViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }

    public bool Initialise(IDictionary<string, object> data)
    {
        if (data.ContainsKey(nameof(Note))
            && data[nameof(Note)] is ObservableNote note)
        {
            _viewModel.Note = note;
            return true;
        }
        return false;
    }

    protected override void OnDismissedByTappingOutsideOfPopup()
    {
        base.OnDismissedByTappingOutsideOfPopup();
        _viewModel.Reset();
    }
}