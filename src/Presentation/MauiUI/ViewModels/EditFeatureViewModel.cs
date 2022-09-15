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
    public partial class EditFeatureViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly ITextResource _textResource;
        private readonly IDictionary<string, string> _properties = new Dictionary<string, string>();

        [ObservableProperty]
        private int? _weight = null;

        [ObservableProperty]
        private int? _value;

        [ObservableProperty]
        private string? _selectedProperty;

        private ObservableFeature _feature = ObservableFeature.New();
        private readonly ObservableFeature _backup = ObservableFeature.New();

        public EditFeatureViewModel(IMediator mediator
            , ITextResource textResource)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
            UpdateProperties();
        }

        public ObservableFeature Feature
        {
            get => _feature;
            set => SetProperty(ref _feature, value, () => { SaveState(_feature, _backup); });
        }

        public IList<string> Types => Enum.GetNames(typeof(FeatureTypes));

        public IList<string> Properties { get; } = new List<string>();

        [RelayCommand]
        public async Task SaveAndClose()
        {
            if (!string.IsNullOrWhiteSpace(SelectedProperty)
                && (Value != null && Value != 0))
            {
                Feature.Properties.Add(_properties[SelectedProperty], (int)Value);
            }

            if (Weight != null)
            {
                Feature.Properties.Add(AttributeNames.HEAVY, (int)Weight);
            }

            await _mediator.Send(NavRequest.Close(Feature));
        }

        public void Reset()
        {
            SaveState(_backup, _feature);
            _feature.PropertiesChanged();
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

        public static void SaveState(ObservableFeature from, ObservableFeature to)
        {
            to.Name = from.Name;
            to.Details = from.Details;
            to.Quantity = from.Quantity;
            to.Value = from.Value;
            to.FeatureType = from.FeatureType;
            to.Keywords = from.Keywords;
            to.NextAdventure = from.NextAdventure;
            to.Properties.Clear();
            foreach (var item in from.Properties)
            {
                to.Properties.Add(item);
            }
        }
    }
}