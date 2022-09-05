using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using static Java.Util.Jar.Attributes;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class UpdateAttributeViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        private ObservableAttribute _attribute;

        private int _originalValue;

        [ObservableProperty]
        private int? _updateValue;

        public UpdateAttributeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public int Value => Attribute?.Value ?? 0;

        public ObservableAttribute Attribute
        {
            get => _attribute;
            set
            {
                SetProperty(ref _attribute, value);
                _originalValue = _attribute.Value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public void Reset()
        {
            _attribute.Value = _originalValue;
        }

        [RelayCommand]
        private void SaveAndClose()
        {
            _navigationService.Pop(true);
        }

        [RelayCommand]
        private void Overwrite()
        {
            if (UpdateValue == null)
            {
                return;
            }

            Attribute.Value = (int)UpdateValue;
            OnPropertyChanged(nameof(Value));
        }

        [RelayCommand]
        private void UpdateAttribute(bool addition = true)
        {
            if (UpdateValue == null)
            {
                return;
            }

            if (addition)
            {
                Attribute.Value += (int)UpdateValue;
            }
            else
            {
                Attribute.Value -= (int)UpdateValue;
            }
            UpdateValue = null;
            OnPropertyChanged(nameof(Value));
        }
    }
}