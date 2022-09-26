using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Infrastructure.Messages;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class FeaturesViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationState _state;
        private readonly IMessenger _messenger;

        [ObservableProperty]
        private ObservableFeature? _selectedFeature;

        public FeaturesViewModel(IMediator mediator
            , IApplicationState state
            , IMessenger messenger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _state = state ?? throw new ArgumentNullException(nameof(state));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

            _state.PropertyChanged += State_PropertyChanged;

            _messenger.Register<RemoveFeature>(this, HandleRemoveFeature);
        }

        public ObservableCollection<ObservableFeature> Features => Character.Features;

        public ObservableCharacter Character => _state.Character;

        public bool CharacterLoaded => _state.CharacterLoaded;

        [RelayCommand]
        public async Task AddFeature()
        {
            await _mediator.Send(NavRequest.CreateFeature());
        }

        [RelayCommand]
        public async Task EditFeature(ObservableFeature feature)
        {
            var keys = feature.Properties.Select(x => x.Key).ToList();
            await _mediator.Send(NavRequest.EditFeature(feature));

            keys.AddRange(feature.Properties.Select(x => x.Key));
            UpdateProperties(keys);

            //TODO: update this
            //Character.UpdateKeywords();
        }

        [RelayCommand]
        public async Task ShowCharacter() => await _mediator.Send(NavRequest.ShowCharacter());

        [RelayCommand]
        public async Task ShowNotes() => await _mediator.Send(NavRequest.ShowNotes());

        private void UpdateProperties(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                //Character.ValueChanged(key);
            }
        }

        private void State_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Character")
            {
                OnPropertyChanged(nameof(Character));
                OnPropertyChanged(nameof(CharacterLoaded));
                OnPropertyChanged(nameof(Features));
            }
        }

        private void HandleRemoveFeature(object recipient, RemoveFeature message)
        {
            Features.Remove(message.Feature);
        }
    }
}