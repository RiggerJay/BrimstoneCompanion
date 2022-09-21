using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class FeaturesViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationState _state;

        [ObservableProperty]
        private ObservableFeature? _selectedFeature;

        public FeaturesViewModel(IMediator mediator, IApplicationState state)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _state = state ?? throw new ArgumentNullException(nameof(state));
            _state.PropertyChanged += State_PropertyChanged;
        }

        public ObservableCollection<ObservableFeature> Features => Character.Features;

        public ObservableCharacter Character => _state.Character;

        public bool CharacterLoaded => _state.CharacterLoaded;

        [RelayCommand]
        public async Task AddFeature()
        {
            var feature = await _mediator.Send(NavRequest.CreateFeature());

            /*if (feature != null)
            {
                var keys = feature.Properties.Select(x => x.Key).ToList();
                keys.AddRange(feature.Properties.Select(x => x.Key));
                UpdateProperties(keys);
                Features.Add(feature);
                Character.UpdateKeywords();
                await SaveCharacter();
            }*/
        }

        [RelayCommand]
        public async Task EditFeature(ObservableFeature feature)
        {
            var keys = feature.Properties.Select(x => x.Key).ToList();
            if (await _mediator.Send(NavRequest.EditFeature(feature)))
            {
                keys.AddRange(feature.Properties.Select(x => x.Key));
                UpdateProperties(keys);
                Character.UpdateKeywords();
                await SaveCharacter();
            }
        }

        [RelayCommand]
        private async Task DeleteFeature(ObservableFeature? feature)
        {
            if (feature == null)
            {
                return;
            }

            if (await _mediator.Send(BoolAlertRequest.WithTitleAndMessage("Are you sure?", "You will lose this Feature for good.")))
            {
                UpdateProperties(feature.Properties.Select(x => x.Key));
                Features.Remove(feature);
                await SaveCharacter();
            }
        }

        [RelayCommand]
        public async Task ShowCharacter() => await _mediator.Send(NavRequest.ShowCharacter());

        [RelayCommand]
        public async Task ShowNotes() => await _mediator.Send(NavRequest.ShowNotes());

        private void UpdateProperties(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                Character.ValueChanged(key);
            }
        }

        private Task SaveCharacter() =>
            _mediator.Send(SaveCharacterRequest.Save());

        private void State_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Character")
            {
                OnPropertyChanged(nameof(Character));
                OnPropertyChanged(nameof(CharacterLoaded));
                OnPropertyChanged(nameof(Features));
            }
        }
    }
}