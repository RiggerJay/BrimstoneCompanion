using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class IncrementAttributeViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ITextResource _textResource;

        private ObservableAttribute? _attribute;

        private int _originalValue;
        private int? _originalMaxValue;

        public IncrementAttributeViewModel(INavigationService navigationService
            , ITextResource textResource)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
        }

        public int Value => Attribute?.Value ?? 0;

        public int? MaxValue => Attribute?.MaxValue;

        public bool HasMaxValue => Attribute?.MaxValue != null;

        public string Name => _textResource.GetValue(Attribute?.Key ?? string.Empty);

        public ObservableAttribute? Attribute
        {
            get => _attribute;
            set
            {
                SetProperty(ref _attribute, value);
                _originalValue = _attribute.Value;
                _originalMaxValue = _attribute.MaxValue;
                OnPropertyChanged(nameof(Value));
                OnPropertyChanged(nameof(MaxValue));
                OnPropertyChanged(nameof(HasMaxValue));
                OnPropertyChanged(nameof(Name));
            }
        }

        public void Reset()
        {
            if (Attribute == null)
            {
                return;
            }

            Attribute.Value = _originalValue;
            Attribute.MaxValue = _originalMaxValue;
        }

        [RelayCommand]
        private void SaveAndClose()
        {
            _navigationService.Pop(true);
        }

        [RelayCommand]
        private void IncrementValue(bool addition = true)
        {
            if (Attribute == null)
            {
                return;
            }

            var value = Attribute.Value + (addition ? 1 : -1);

            if (Attribute.MaxValue != null
                && value > Attribute.MaxValue)
            {
                return;
            }

            Attribute.Value = value;

            OnPropertyChanged(nameof(Value));
        }

        [RelayCommand]
        private void IncrementMaxValue(bool addition = true)
        {
            if (Attribute == null)
            {
                return;
            }

            Attribute.MaxValue += addition ? 1 : -1;

            if (Attribute.Value > Attribute.MaxValue)
            {
                Attribute.Value = (int)Attribute.MaxValue;
            }

            OnPropertyChanged(nameof(MaxValue));
            OnPropertyChanged(nameof(Value));
        }
    }
}