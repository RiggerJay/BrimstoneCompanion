using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class NewCharacterViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        private string _name;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        private string _class;

        public NewCharacterViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            CreateCommand = new AsyncRelayCommand(CreateAsync, CanCreate);
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

        public AsyncRelayCommand CreateCommand { get; }

        public async Task CreateAsync()
        {
            await _mediator.Send(NavRequest.Close(new ObservableCharacter
            {
                Name = _name,
                Class = _class,
            }));
        }

        public bool CanCreate()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Class);
        }
    }
}