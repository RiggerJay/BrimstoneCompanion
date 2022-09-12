using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Java.Util.Jar;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
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
            var character = new ObservableCharacter
            {
                Name = _name,
                Class = _class,
            };

            SetAttributes(character);

            await _mediator.Send(NavRequest.Close(character));
        }

        private void SetAttributes(ObservableCharacter character)
        {
            character.SetAttribute(AttributeNames.XP, 2);
            character.SetAttribute(AttributeNames.GRIT, 2, 2);
            character.SetAttribute(AttributeNames.CORRUPTION, 2, 5);
            character.SetAttribute(AttributeNames.HEAVY, 2);
            character.SetAttribute(AttributeNames.AGILITY, 2);
            character.SetAttribute(AttributeNames.CUNNING, 2);
            character.SetAttribute(AttributeNames.SPIRIT, 2);
            character.SetAttribute(AttributeNames.STRENGTH, 2);
            character.SetAttribute(AttributeNames.LORE, 2);
            character.SetAttribute(AttributeNames.LUCK, 2);
            character.SetAttribute(AttributeNames.COMBAT, 2);
            character.SetAttribute(AttributeNames.INITIATIVE, 2);
            character.SetAttribute(AttributeNames.MELEE, 4);
            character.SetAttribute(AttributeNames.RANGE, 4);
            character.SetAttribute(AttributeNames.WOUNDS, 2);
            character.SetAttribute(AttributeNames.HEALTH, 2);
            character.SetAttribute(AttributeNames.HORROR, 2);
            character.SetAttribute(AttributeNames.SANITY, 2);
            character.SetAttribute(AttributeNames.DEFENCE, 4);
            character.SetAttribute(AttributeNames.WILLPOWER, 4);
            character.SetAttribute(AttributeNames.DOLLARS, 2);
            character.SetAttribute(AttributeNames.DARKSTONE, 2);
        }

        public bool CanCreate()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Class);
        }
    }
}