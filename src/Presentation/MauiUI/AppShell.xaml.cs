using RedSpartan.BrimstoneCompanion.MauiUI.Pages;

namespace RedSpartan.BrimstoneCompanion.MauiUI;

public partial class AppShell : Shell
{
    public AppShell(CharacterSelectorPage page)
    {
        InitializeComponent();
        CurrentItem = page;
    }
}