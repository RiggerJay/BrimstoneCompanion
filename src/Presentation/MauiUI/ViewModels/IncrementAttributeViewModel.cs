using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class IncrementAttributeViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly ITextResource _textResource;

        private ObservableAttribute _attribute;

        private int _originalValue;
        private int? _originalMaxValue;

        public IncrementAttributeViewModel(IMediator mediator
            , ITextResource textResource)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
        }

        public int Value => Attribute?.Value ?? 0;

        public int? MaxValue => Attribute?.MaxValue;

        public bool HasMaxValue => Attribute?.HasMaxValue ?? false;

        public string Name => _textResource.GetValue(Attribute?.Key ?? string.Empty);

        public ObservableAttribute Attribute
        {
            get => _attribute;
            set
            {
                SetProperty(ref _attribute, value);
                _originalValue = _attribute.Value;
                _originalMaxValue = _attribute.MaxValue;
                OnPropertyChanged(nameof(Name));
            }
        }

        public async void Reset()
        {
            if (Attribute == null)
            {
                return;
            }
            await _mediator.Send(UpdateAttributeValueRequest.With(Attribute, _originalValue));

            if (_originalMaxValue.HasValue)
            {
                await _mediator.Send(UpdateAttributeMaxValueRequest.With(Attribute, _originalMaxValue.Value));
            }
        }

        [RelayCommand]
        private async Task SaveAndClose()
        {
            await _mediator.Send(NavRequest.Close(true));
        }

        [RelayCommand]
        private async Task IncrementValue(bool addition = true)
        {
            if (Attribute == null)
            {
                return;
            }
            await _mediator.Send(UpdateAttributeValueRequest.With(Attribute, Attribute.Value + (addition ? 1 : -1)));
        }

        [RelayCommand]
        private async Task IncrementMaxValue(bool addition = true)
        {
            if (Attribute == null || !Attribute.MaxValue.HasValue)
            {
                return;
            }
            await _mediator.Send(UpdateAttributeMaxValueRequest.With(Attribute, Attribute.MaxValue.Value + (addition ? 1 : -1)));
        }
    }
}