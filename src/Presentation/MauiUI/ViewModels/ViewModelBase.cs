using CommunityToolkit.Mvvm.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        private bool _isBusy = false;
        private string _title = string.Empty;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}