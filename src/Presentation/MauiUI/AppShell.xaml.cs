using RedSpartan.BrimstoneCompanion.MauiUI.Pages;

namespace RedSpartan.BrimstoneCompanion.Presentation.MauiUI;

public partial class AppShell : Shell
{
    public AppShell(CharacterSelectorPage page)
    {
        InitializeComponent();
        CurrentItem = page;
    }
}