﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Feature), nameof(Feature))]
    public partial class EditFeatureViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly ITextResource _textResource;
        private readonly IApplicationState _state;
        private readonly IDictionary<string, string> _properties = new Dictionary<string, string>();

        private bool _saved = false;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnterPropertyCommand))]
        private string? _value;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnterPropertyCommand))]
        private string? _selectedProperty;

        private string _keyword;

        private ObservableFeature _feature = ObservableFeature.New();
        private readonly ObservableFeature _backup = ObservableFeature.New();

        public EditFeatureViewModel(IMediator mediator
            , ITextResource textResource
            , IApplicationState state)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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

        private void LoadNewFeature()
        {
            Keys.Clear();

            Keys.AddRange(_feature.Properties.Select(x => x.Key));

            SaveState(_feature, _backup);
        }

        public ObservableFeature Feature
        {
            get => _feature;
            set => SetProperty(ref _feature, value, LoadNewFeature);
        }

        public IList<string> Types => Enum.GetNames(typeof(FeatureTypes));

        public IList<string> Properties { get; } = new List<string>();

        public List<string> Keys { get; } = new List<string>();

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
                Feature.Keywords.Add(ObservableKeyword.New(Keyword.Trim(), false));
                Keyword = string.Empty;
            }
        }

        private bool CanEnterKeyword() => !string.IsNullOrWhiteSpace(_keyword);

        [RelayCommand(CanExecute = nameof(CanEnterProperty))]
        private void EnterProperty()
        {
            if (CanEnterProperty()
                && int.TryParse(Value, out int value))
            {
                Feature.AddProperty(_properties[SelectedProperty], value);

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
            EnterProperty();

            EnterKeyword();

            Keys.AddRange(Feature.Properties.Select(x => x.Key));

            UpdateProperties(Keys);

            _state.Character.UpdateKeywords();
            await _mediator.Send(SaveCharacterRequest.Save());
            _saved = true;
            await _mediator.Send(NavRequest.Close());
        }

        private void UpdateProperties(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                _state.Character.ValueChanged(key);
            }
        }

        public void Reset()
        {
            if (_saved)
            {
                return;
            }
            SaveState(_backup, _feature);
            _feature.PropertiesChanged();
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
                to.AddProperty(item.Key, item.Value);
            }

            to.Keywords.Clear();
            foreach (var item in from.Keywords)
            {
                to.AddKeyword(item.Word);
            }
        }
    }
}