using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class UpdateAttributeViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Value))]
        private ObservableAttribute _attribute;

        [ObservableProperty]
        private int? _updateValue;

        public UpdateAttributeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public int Value => Attribute.Value;

        [RelayCommand]
        public void SaveAndClose()
        {
            _navigationService.Pop(true);
        }

        [RelayCommand]
        public void UpdateAttribute()
        {
            if (UpdateValue == null)
            {
                return;
            }

            if (true)
            {
                Attribute.Value += (int)UpdateValue;
            }
            else
            {
                Attribute.Value -= (int)UpdateValue;
            }
        }
    }
}