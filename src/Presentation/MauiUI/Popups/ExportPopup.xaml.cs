using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class ExportPopup : CommunityToolkit.Maui.Views.Popup, IInitialisePopup
{
    private readonly ExportViewModel _viewModel;

    public ExportPopup(ExportViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }

    public bool Initialise(IDictionary<string, object> data)
    {
        if (data.ContainsKey(nameof(Character))
            && data[nameof(Character)] is ObservableCharacter character)
        {
            _viewModel.Character = character;
            return true;
        }
        return false;
    }
}