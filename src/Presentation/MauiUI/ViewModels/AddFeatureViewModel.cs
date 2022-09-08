using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class AddFeatureViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public AddFeatureViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        public ObservableFeature Feature { get; } = ObservableFeature.New();

        public IList<string> Types => Enum.GetNames(typeof(FeatureTypes));

        [RelayCommand]
        public void SaveAndClose()
        {
            _navigationService.Pop(Feature);
        }
    }
}