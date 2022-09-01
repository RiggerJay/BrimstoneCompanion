using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RedSpartan.BrimstoneCompanion.Presentation.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TextValue))]
        private int _counter;

        public string TextValue => Counter == 1
            ? $"Clicked {Counter} time"
            : $"Clicked {Counter} times";

        [RelayCommand]
        private void CounterClicked() => Counter++;
    }
}