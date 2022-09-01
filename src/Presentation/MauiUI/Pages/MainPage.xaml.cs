using RedSpartan.BrimstoneCompanion.MauiUI.ViewModels;

namespace RedSpartan.BrimstoneCompanion.Presentation.MauiUI.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = mainViewModel;
    }
}