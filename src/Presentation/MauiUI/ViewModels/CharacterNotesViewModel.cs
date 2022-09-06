using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Features), nameof(Features))]
    public class CharacterNotesViewModel : ViewModelBase
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

        public void AddFeature()
        {
        }
    }
}