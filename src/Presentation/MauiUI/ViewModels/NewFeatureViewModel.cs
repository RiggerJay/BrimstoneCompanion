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

        public NewFeatureViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public ObservableFeature Feature { get; } = ObservableFeature.New();

        public IList<string> Types => Enum.GetNames(typeof(FeatureTypes));

        public IList<string> Properties { get; } = AttributeNames.Strings;

        public int? Value { get; set; }

        [RelayCommand]
        public void SaveAndClose()
        {
            _navigationService.Pop(Feature);
        }
    }
}