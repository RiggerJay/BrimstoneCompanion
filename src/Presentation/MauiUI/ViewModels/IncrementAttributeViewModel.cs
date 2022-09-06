using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class IncrementAttributeViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ITextResource _textResource;

        private ObservableAttribute _attribute;

        private int _originalValue;

        public IncrementAttributeViewModel(INavigationService navigationService
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
        private void IncrementAttribute(bool addition = true)
        {
            Attribute.Value += addition ? 1 : -1;

            OnPropertyChanged(nameof(Value));
        }
    }
}