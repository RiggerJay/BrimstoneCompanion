using CommunityToolkit.Mvvm.ComponentModel;
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
        private readonly IDictionary<string, string> _properties = new Dictionary<string, string>();

        [ObservableProperty]
        private string? _weight = null;

        [ObservableProperty]
        private string? _value;

        [ObservableProperty]
        private string? _selectedProperty;

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

        [RelayCommand]
        public async Task SaveAndClose()
        {
            if (!string.IsNullOrWhiteSpace(SelectedProperty)
                && !string.IsNullOrWhiteSpace(Value)
                && int.TryParse(Value, out int value))
            {
                Feature.AddProperty(_properties[SelectedProperty], value);
            }

            if (!string.IsNullOrWhiteSpace(Weight)
                && int.TryParse(Weight, out int weight))
            {
                Feature.AddProperty(AttributeNames.HEAVY, weight);
            }

            await _mediator.Send(NavRequest.Close(Feature));
        }
    }
}