using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class UpdateAttributeViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly ITextResource _textResource;

        private ObservableAttribute _attribute;

        private int _originalValue;

        [ObservableProperty]
        private int? _updateValue;

        public UpdateAttributeViewModel(IMediator mediator
            , ITextResource textResource)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
        private async Task SaveAndClose()
        {
            if (UpdateValue != null)
            {
                Attribute.Value += (int)UpdateValue;
            }
            await _mediator.Send(NavRequest.Close(true));
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
            Attribute.Value += GetValue(UpdateValue, addition);

            ResetValue();
        }

        private void ResetValue()
        {
            UpdateValue = null;
            OnPropertyChanged(nameof(Value));
        }

        private static int GetValue(int? updateValue, bool addition)
        {
            if (updateValue == null)
            {
                return addition ? 1 : -1;
            }

            return addition ? (int)updateValue : (int)updateValue * -1;
        }
    }
}