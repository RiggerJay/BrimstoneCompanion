using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class IncrementAttributePopup : CommunityToolkit.Maui.Views.Popup, IInitialisePopup
{
    private readonly IncrementAttributeViewModel _viewModel;

    public IncrementAttributePopup(IncrementAttributeViewModel viewModel)
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

    protected override void OnDismissedByTappingOutsideOfPopup()
    {
        base.OnDismissedByTappingOutsideOfPopup();
        _viewModel.Reset();
    }
}