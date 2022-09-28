using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

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

        public async void Reset()
        {
            await _mediator.Send(UpdateAttributeValueRequest.With(Attribute, _originalValue));
        }

        [RelayCommand]
        private async Task Overwrite()
        {
            if (UpdateValue == null)
            {
                return;
            }

            await _mediator.Send(UpdateAttributeValueRequest.With(Attribute, UpdateValue.Value));

            await _mediator.Send(NavRequest.Close(true));
        }

        [RelayCommand]
        private async Task UpdateAttribute(bool addition = true)
        {
            await _mediator.Send(UpdateAttributeValueRequest.With(Attribute, GetValue(UpdateValue, addition)));

            await _mediator.Send(NavRequest.Close(true));
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