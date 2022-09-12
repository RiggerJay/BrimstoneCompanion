using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class NewFeatureViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly ITextResource _textResource;
        private string? _selectedProperty;
        private int? _value;
        private readonly IDictionary<string, string> _properties = new Dictionary<string, string>();

        public NewFeatureViewModel(IMediator mediator
            , ITextResource textResource)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            foreach (var item in AttributeNames.Strings)
            {
                Properties.Add(_textResource.GetValue(item));
                _properties.Add(_textResource.GetValue(item), item);
            }
            OnPropertyChanged(nameof(Properties));
        }

        public ObservableFeature Feature { get; } = ObservableFeature.New();

        public IList<string> Types => Enum.GetNames(typeof(FeatureTypes));

        public IList<string> Properties { get; } = new List<string>();

        public string? SelectedProperty { get => _selectedProperty; set => SetProperty(ref _selectedProperty, value); }

        public int? Value { get => _value; set => SetProperty(ref _value, value); }

        [RelayCommand]
        public async Task SaveAndClose()
        {
            if (!string.IsNullOrWhiteSpace(SelectedProperty)
                && (Value != null && Value != 0))
            {
                Feature.Properties.Add(_properties[SelectedProperty], (int)Value);
            }

            await _mediator.Send(NavRequest.Close(Feature));
        }
    }
}