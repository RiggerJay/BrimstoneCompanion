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
                Features.Add(feature);
                await SaveCharacter();
            }
        }

        [RelayCommand]
        private async Task DeleteFeature(ObservableFeature? feature)
        {
            if (feature != null
                && await _mediator.Send(BoolAlertRequest.WithTitleAndMessage("Are you sure?", "You will lose this Feature for good.")))
            {
                Features.Remove(feature);
                await SaveCharacter();
            }
        }

        [RelayCommand]
        public async Task EditFeature(ObservableFeature feature)
        {
            if (await _mediator.Send(NavRequest.EditFeature(feature)))
            {
                await SaveCharacter();
            }
        }

        private Task SaveCharacter() =>
            _mediator.Send(SaveRequest<ObservableCharacter>.With(Character));
    }
}