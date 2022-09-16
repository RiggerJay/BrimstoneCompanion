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
        private string? _weight = null;

        [ObservableProperty]
        private string? _value;

        [ObservableProperty]
        private string? _selectedProperty;

        private string _keyword;

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
            set => SetProperty(ref _feature, value, NewFeatureAdded);
        }

        public IList<string> Types => Enum.GetNames(typeof(FeatureTypes));

        public IList<string> Properties { get; } = new List<string>();

        [RelayCommand]
        public async Task SaveAndClose()
        {
            Feature.Properties.Clear();
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

            await _mediator.Send(NavRequest.Close(true));
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
        }

        private void NewFeatureAdded()
        {
            if (_feature.Properties.Any(x => x.Key == AttributeNames.HEAVY))
            {
                Weight = _feature.Properties.First(x => x.Key == AttributeNames.HEAVY).Value.ToString();
            }

            foreach (var prop in _feature.Properties)
            {
                if (prop.Key != AttributeNames.HEAVY)
                {
                    SelectedProperty = _textResource.GetValue(prop.Key);
                    Value = prop.Value.ToString();
                }
            }

            SaveState(_feature, _backup);
        }

        public string Keyword
        {
            get => _keyword;
            set => SetProperty(ref _keyword, value, EnterKeyword);
        }

        private void EnterKeyword()
        {
            if (!string.IsNullOrWhiteSpace(Keyword)
                && Keyword.EndsWith(' '))
            {
                Feature.Keywords.Add(ObservableKeyword.New(Keyword.Trim(), false));
                Keyword = string.Empty;
            }
        }

        [RelayCommand]
        public void DeleteKeyword(ObservableKeyword keyword)
        {
            Feature.Keywords.Remove(keyword);
        }

        public static void SaveState(ObservableFeature from, ObservableFeature to)
        {
            to.Name = from.Name;
            to.Details = from.Details;
            to.Quantity = from.Quantity;
            to.Value = from.Value;
            to.FeatureType = from.FeatureType;
            to.NextAdventure = from.NextAdventure;
            to.Properties.Clear();
            foreach (var item in from.Properties)
            {
                to.Properties.Add(item);
            }
        }
    }
}