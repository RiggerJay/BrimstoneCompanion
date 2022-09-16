using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Character), nameof(Character))]
    public partial class FeatureViewModel : ViewModelBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Features))]
        private ObservableCharacter _character;

        [ObservableProperty]
        private ObservableFeature? _selectedFeature;

        private readonly IMediator _mediator;

        public FeatureViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ObservableCollection<ObservableFeature> Features => Character?.Features;

        [RelayCommand]
        public async Task AddFeature()
        {
            var feature = await _mediator.Send(NavRequest.CreateFeature());

            if (feature != null)
            {
                var keys = feature.Properties.Select(x => x.Key).ToList();
                keys.AddRange(feature.Properties.Select(x => x.Key));
                UpdateProperties(keys);
                Features.Add(feature);
                Character.UpdateKeywords();
                await SaveCharacter();
            }
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

        private void UpdateProperties(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                Character.ValueChanged(key);
            }
        }

        private Task SaveCharacter() =>
            _mediator.Send(SaveRequest<ObservableCharacter>.With(Character));
    }
}