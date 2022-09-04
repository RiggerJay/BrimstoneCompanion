using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class UpdateAttributePopup : CommunityToolkit.Maui.Views.Popup, IInitialisePopup
{
    private readonly UpdateAttributeViewModel _viewModel;

    public UpdateAttributePopup(UpdateAttributeViewModel viewModel)
    {
        BindingContext = _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
    }

    public bool Initialise(IDictionary<string, object> data)
    {
        if (data.ContainsKey(nameof(Attribute))
            && data[nameof(Attribute)] is ObservableAttribute attribute)
        {
            _viewModel.Attribute = attribute;
            return true;
        }
        return false;
    }
}