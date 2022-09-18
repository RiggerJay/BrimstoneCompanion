using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI;

public partial class TabAppShell : Shell
{
    public TabAppShell(ShellViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}