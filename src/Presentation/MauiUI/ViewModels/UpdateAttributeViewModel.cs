using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class UpdateAttributeViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ITextResource _textResource;

        private ObservableAttribute _attribute;

        private int _originalValue;

        [ObservableProperty]
        private int? _updateValue;

        public UpdateAttributeViewModel(INavigationService navigationService
            , ITextResource textResource)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
        }

        public int Value => Attribute?.Value ?? 0;
        public string Name => _textResource.GetValue(Attribute?.Key ?? string.Empty);

        public ObservableAttribute Attribute
        {
            get => _attribute;
            set
            {
                SetProperty(ref _attribute, value);
                _originalValue = _attribute.Value;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(Name));
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
            ResetValue();
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
            ResetValue();
        }

        private void ResetValue()
        {
            UpdateValue = null;
            OnPropertyChanged(nameof(Value));
        }
    }
}