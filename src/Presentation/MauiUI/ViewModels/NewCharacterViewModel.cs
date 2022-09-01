using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class NewCharacterViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        private string _name;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        private string _class;

        public NewCharacterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            CreateCommand = new RelayCommand(Create, CanCreate);
        }

        public IList<string> Classes { get; } = new List<string>
        {
            "Assassin",
            "Bandito",
            "Cowboy",
            "Dark Stone Shaman",
            "Drifter",
            "Frontier Doc",
            "Gambler",
            "Gun Slinger",
            "Indian Scout",
            "Jargono Native",
            "Jargono Tribal Human",
            "Lawman",
            "Orphan",
            "Outlaw",
            "Preacher",
            "Prospector",
            "Rancher",
            "Saloon Girl",
            "Samurai Warrior",
            "Sorceress",
            "Traveling Monk",
            "U.S. Marshal",
            "Wandering Samurai"
        };

        public RelayCommand CreateCommand { get; }

        public void Create()
        {
            _navigationService.Pop(new ObservableCharacter
            {
                Name = _name,
                Class = _class,
            });
        }

        public bool CanCreate()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Class);
        }
    }
}