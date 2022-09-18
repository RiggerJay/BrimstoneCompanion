namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private bool _showSelectorTab = true;

        public bool ShowSelectorTab
        {
            get => _showSelectorTab;
            set => SetProperty(ref _showSelectorTab, value);
        }
    }
}