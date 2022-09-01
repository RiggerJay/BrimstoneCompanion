using RedSpartan.BrimstoneCompanion.Presentation.ViewModels;

namespace RedSpartan.BrimstoneCompanion.Presentation.MauiUI.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
    }
}