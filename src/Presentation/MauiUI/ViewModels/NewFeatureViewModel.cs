using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class NewFeatureViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private string? _selectedProperty;
        private int? _value;

        public NewFeatureViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public ObservableFeature Feature { get; } = ObservableFeature.New();

        public IList<string> Types => Enum.GetNames(typeof(FeatureTypes));

        public IList<string> Properties { get; } = AttributeNames.Strings;

        public string? SelectedProperty { get => _selectedProperty; set => SetProperty(ref _selectedProperty, value); }

        public int? Value { get => _value; set => SetProperty(ref _value, value); }

        [RelayCommand]
        public void SaveAndClose()
        {
            if (!string.IsNullOrEmpty(SelectedProperty)
                && (Value != null && Value != 0))
            {
                Feature.Properties.Add(SelectedProperty, (int)Value);
            }

            _navigationService.Pop(Feature);
        }
    }
}