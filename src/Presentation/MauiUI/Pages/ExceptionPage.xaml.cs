namespace RedSpartan.BrimstoneCompanion.MauiUI.Pages;

public partial class ExceptionPage : ContentPage
{
	public ExceptionPage()
	{
		InitializeComponent();
	}

	public string ExceptionMessage { get; private set; }

    public ExceptionPage SetException(Exception ex)
	{
		ExceptionMessage = ex.Message;
		OnPropertyChanged(nameof(ExceptionMessage));
		return this;
	}
}