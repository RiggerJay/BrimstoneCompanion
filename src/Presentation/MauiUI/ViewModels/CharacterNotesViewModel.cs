using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.Popups;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Features), nameof(Features))]
    public partial class CharacterNotesViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ObservableCollection<ObservableFeature> _features;

        public CharacterNotesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public ObservableCollection<ObservableFeature> Features
        {
            get => _features;
            set => SetProperty(ref _features, value);
        }

        [RelayCommand]
        public async Task AddFeature()
        {
            var feature = await _navigationService.PushAsync<NewFeaturePopup, ObservableFeature?>();

            if (feature != null)
            {
                Features.Add(feature);
            }
        }
    }
}