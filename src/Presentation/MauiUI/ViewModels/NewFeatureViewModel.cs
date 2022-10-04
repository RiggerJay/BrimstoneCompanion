using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class NewFeatureViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly ITextResource _textResource;
        private readonly IDictionary<string, string> _properties = new Dictionary<string, string>();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnterPropertyCommand))]
        private string? _value;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnterPropertyCommand))]
        private string? _selectedProperty;

        [ObservableProperty]
        private string? _cost;

        [ObservableProperty]
        private string? _weight;

        private string _keyword;

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

        public IList<string> PropertiesChanged { get; } = new List<string>();

        public string Keyword
        {
            get => _keyword;
            set => SetProperty(ref _keyword, value, KeywordEntered);
        }

        private void KeywordEntered()
        {
            if (CanEnterKeyword()
                && Keyword.EndsWith(' '))
            {
                EnterKeyword();
            }
            EnterKeywordCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanEnterKeyword))]
        private void EnterKeyword()
        {
            if (CanEnterKeyword())
            {
                Feature.Keywords.Add(ObservableKeyword.New(Keyword.Trim(), false));
                Keyword = string.Empty;
            }
        }

        private bool CanEnterKeyword() => !string.IsNullOrWhiteSpace(Keyword);

        [RelayCommand(CanExecute = nameof(CanEnterProperty))]
        private void EnterProperty()
        {
            if (CanEnterProperty()
                && int.TryParse(Value, out int value))
            {
                Feature.AddProperty(_properties[SelectedProperty], value);
                PropertiesChanged.Add(_properties[SelectedProperty]);
                Value = string.Empty;
                SelectedProperty = string.Empty;
            }
        }

        private bool CanEnterProperty()
            => !string.IsNullOrWhiteSpace(Value)
            && !string.IsNullOrWhiteSpace(SelectedProperty);

        [RelayCommand]
        public void DeleteProperty(ObservableProp prop)
            => Feature.Properties.Remove(prop);

        [RelayCommand]
        public void DeleteKeyword(ObservableKeyword keyword) =>
            Feature.Keywords.Remove(keyword);

        [RelayCommand]
        public async Task SaveAndClose()
        {
            if (string.IsNullOrWhiteSpace(Feature.Name))
            {
                return;
            }

            Feature.Weight = int.TryParse(Weight, out int weight) ? weight : 0;

            Feature.Value = int.TryParse(Cost, out int value) ? value : 0;

            EnterProperty();

            EnterKeyword();

            await _mediator.Send(CreateFeatureRequest.With(Feature));

            await _mediator.Send(SaveCharacterRequest.Save());

            await _mediator.Send(NavRequest.Close());
        }
    }
}