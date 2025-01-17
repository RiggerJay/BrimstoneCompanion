﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.Infrastructure.Messages;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Feature), nameof(Feature))]
    public partial class EditFeatureViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly IMessenger _messenger;
        private readonly ITextResource _textResource;
        private readonly IApplicationState _state;
        private readonly IDictionary<string, string> _properties = new Dictionary<string, string>();
        private readonly IList<string> _keys = new List<string>();
        private bool _saved = false;

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

        private ObservableFeature _feature = ObservableFeature.New();
        private readonly ObservableFeature _backup = ObservableFeature.New();

        public EditFeatureViewModel(IMediator mediator
            , ITextResource textResource
            , IApplicationState state
            , IMessenger messenger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _textResource = textResource ?? throw new ArgumentNullException(nameof(textResource));
            _state = state ?? throw new ArgumentNullException(nameof(state));
            LoadProperties();
        }

        private void LoadProperties()
        {
            foreach (var item in AttributeNames.Strings)
            {
                Properties.Add(_textResource.GetValue(item));
                _properties.Add(_textResource.GetValue(item), item);
            }
            OnPropertyChanged(nameof(Properties));
        }

        public ObservableFeature Feature
        {
            get => _feature;
            set => SetFeature(value);
        }

        public IList<string> Types => Enum.GetNames(typeof(FeatureTypes));

        public IList<string> Properties { get; } = new List<string>();

        public string Keyword
        {
            get => _keyword;
            set => SetProperty(ref _keyword, value, KeywordEntered);
        }

        private void KeywordEntered()
        {
            if (CanEnterKeyword()
                && _keyword.EndsWith(' '))
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
                var keyword = ObservableKeyword.New(Keyword.Trim(), false);
                Feature.Keywords.Add(keyword);
                Keyword = string.Empty;
            }
        }

        private bool CanEnterKeyword()
            => !string.IsNullOrWhiteSpace(_keyword);

        [RelayCommand(CanExecute = nameof(CanEnterProperty))]
        private void EnterProperty()
        {
            if (CanEnterProperty()
                && int.TryParse(Value, out int value))
            {
                Feature.AddProperty(_properties[SelectedProperty], value);
                AddKey(_properties[SelectedProperty]);

                Value = string.Empty;
                SelectedProperty = string.Empty;
            }
        }

        private bool CanEnterProperty()
            => !string.IsNullOrWhiteSpace(Value)
            && !string.IsNullOrWhiteSpace(SelectedProperty);

        [RelayCommand]
        public void DeleteProperty(ObservableProp prop)
        {
            Feature.Properties.Remove(prop);
            AddKey(prop.Key);
        }

        [RelayCommand]
        public void DeleteKeyword(ObservableKeyword keyword)
        {
            Feature.Keywords.Remove(keyword);
        }

        [RelayCommand]
        public async Task SaveAndClose()
        {
            Feature.Weight = int.TryParse(Weight, out int weight) ? weight : 0;

            Feature.Value = int.TryParse(Cost, out int value) ? value : 0;

            _state.Character.CurrentWeight -= _backup.Weight;
            _state.Character.CurrentWeight += Feature.Weight;

            EnterProperty();

            EnterKeyword();

            await _mediator.Send(SaveCharacterRequest.Save());

            await _mediator.Send(RefreshAttributesRequest.With(_keys));

            _messenger.Send(KeywordMessage.Changed());

            _saved = true;
            await _mediator.Send(NavRequest.Close());
        }

        [RelayCommand]
        public async Task DeleteFeature()
        {
            if (await _mediator.Send(BoolAlertRequest.WithTitleAndMessage("Are you sure?", "You will lose this Feature for good.")))
            {
                await RemoveFeatureAsync();
            }
        }

        private async Task RemoveFeatureAsync()
        {
            await _mediator.Send(RemoveFeatureRequest.With(Feature));
            await _mediator.Send(SaveCharacterRequest.Save());
            await _mediator.Send(NavRequest.Close());
        }

        [RelayCommand]
        public async Task SellFeature()
        {
            if (!Feature.HasValue)
            {
                return;
            }

            if (await _mediator.Send(BoolAlertRequest.WithTitleAndMessage("Are you sure?", $"You will sell this Feature for ${Feature.Value}.")))
            {
                await _mediator.Send(UpdateMoneyRequest.WithValue(Feature.Value.Value));
                await RemoveFeatureAsync();
            }
        }

        public void Reset()
        {
            if (_saved)
            {
                return;
            }
            SaveState(_backup, _feature);
        }

        public void SaveState(ObservableFeature from, ObservableFeature to)
        {
            to.Name = from.Name;
            to.Details = from.Details;
            to.Quantity = from.Quantity;
            to.Value = from.Value;
            to.Weight = from.Weight;
            to.FeatureType = from.FeatureType;
            to.NextAdventure = from.NextAdventure;

            foreach (var item in to.Properties)
            {
                AddKey(item.Key);
            }
            to.Properties.Clear();
            foreach (var item in from.Properties)
            {
                to.AddProperty(item.Key, item.Value);
                AddKey(item.Key);
            }

            to.Keywords.Clear();
            foreach (var item in from.Keywords)
            {
                to.AddKeyword(item.Word);
            }
        }

        private void SetFeature(ObservableFeature feature)
        {
            if (SetProperty(ref _feature, feature, nameof(Feature)))
            {
                Weight = feature.Weight == 0 ? string.Empty : feature.Weight.ToString();
                Cost = feature.Value == 0 ? string.Empty : feature.Value.ToString();

                SaveState(_feature, _backup);
            }
        }

        private void AddKey(string key)
        {
            if (!_keys.Contains(key))
            {
                _keys.Add(key);
            }
        }
    }
}