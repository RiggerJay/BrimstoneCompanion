using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.MauiUI.Popups;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Character), nameof(Character))]
    public partial class CharacterViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ICharacterService _characterService;

        #region Fields

        private ObservableCharacter _character;
        private ObservableAttribute _experience;
        private ObservableAttribute _grit;
        private ObservableAttribute _corruption;
        private ObservableAttribute _heavy;

        private ObservableAttribute _agility;
        private ObservableAttribute _cunning;
        private ObservableAttribute _spirit;
        private ObservableAttribute _strength;
        private ObservableAttribute _lore;
        private ObservableAttribute _luck;

        private ObservableAttribute _combat;
        private ObservableAttribute _range;
        private ObservableAttribute _iniative;
        private ObservableAttribute _melee;

        private ObservableAttribute _wounds;
        private ObservableAttribute _health;
        private ObservableAttribute _horror;
        private ObservableAttribute _sanity;
        private ObservableAttribute _defence;
        private ObservableAttribute _willpower;

        private ObservableAttribute _dollars;
        private ObservableAttribute _darkStone;

        #endregion Fields

        public CharacterViewModel(INavigationService navigationService
            , ICharacterService characterService)
        {
            Title = "Character";
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _characterService = characterService ?? throw new ArgumentNullException(nameof(characterService));

            _experience = ObservableAttribute.New(AttributeNames.XP, 0);

            _grit = ObservableAttribute.New(AttributeNames.GRIT, 1, 2);
            _corruption = ObservableAttribute.New(AttributeNames.CORRUPTION, 0, 5);
            _heavy = ObservableAttribute.New(AttributeNames.HEAVY, 5);

            _agility = ObservableAttribute.New(AttributeNames.AGILITY, 2);
            _cunning = ObservableAttribute.New(AttributeNames.CUNNING, 2);
            _spirit = ObservableAttribute.New(AttributeNames.SPIRIT, 2);
            _strength = ObservableAttribute.New(AttributeNames.STRENGTH, 2);
            _lore = ObservableAttribute.New(AttributeNames.LORE, 2);
            _luck = ObservableAttribute.New(AttributeNames.LUCK, 2);

            _combat = ObservableAttribute.New(AttributeNames.COMBAT, 2);
            _range = ObservableAttribute.New(AttributeNames.RANGE, 4);
            _iniative = ObservableAttribute.New(AttributeNames.INITIATIVE, 5);
            _melee = ObservableAttribute.New(AttributeNames.MELEE, 4);

            _wounds = ObservableAttribute.New(AttributeNames.WOUNDS, 0);
            _health = ObservableAttribute.New(AttributeNames.HEALTH, 10);
            _horror = ObservableAttribute.New(AttributeNames.HORROR, 0);
            _sanity = ObservableAttribute.New(AttributeNames.SANITY, 10);
            _defence = ObservableAttribute.New(AttributeNames.DEFENCE, 4);
            _willpower = ObservableAttribute.New(AttributeNames.WILLPOWER, 4);

            _dollars = ObservableAttribute.New(AttributeNames.DOLLARS, 0);
            _darkStone = ObservableAttribute.New(AttributeNames.DARKSTONE, 0);
        }

        public ObservableCharacter Character
        {
            get => _character;
            set
            {
                if (SetProperty(ref _character, value))
                {
                    Title = $"{_character.Name} a level {_character.Level} {_character.Class}";
                }
                OnPropertyChanged(nameof(Experience));
                OnPropertyChanged(nameof(Grit));
                OnPropertyChanged(nameof(Corruption));
                OnPropertyChanged(nameof(Heavy));

                OnPropertyChanged(nameof(Agility));
                OnPropertyChanged(nameof(Cunning));
                OnPropertyChanged(nameof(Spirit));
                OnPropertyChanged(nameof(Strength));
                OnPropertyChanged(nameof(Lore));
                OnPropertyChanged(nameof(Luck));

                OnPropertyChanged(nameof(Combat));
                OnPropertyChanged(nameof(Range));
                OnPropertyChanged(nameof(Initiative));
                OnPropertyChanged(nameof(Melee));

                OnPropertyChanged(nameof(Wounds));
                OnPropertyChanged(nameof(Health));
                OnPropertyChanged(nameof(Horror));
                OnPropertyChanged(nameof(Sanity));
                OnPropertyChanged(nameof(Defence));
                OnPropertyChanged(nameof(Willpower));

                OnPropertyChanged(nameof(Dollars));
                OnPropertyChanged(nameof(DarkStone));

                Task.Run(async () => await _characterService.SaveAsync(_character));
            }
        }

        #region Observable Attribute

        public ObservableAttribute Experience => GetAttribute(AttributeNames.XP, ref _experience);
        public ObservableAttribute Grit => GetAttribute(AttributeNames.GRIT, ref _grit);
        public ObservableAttribute Corruption => GetAttribute(AttributeNames.CORRUPTION, ref _corruption);
        public ObservableAttribute Heavy => GetAttribute(AttributeNames.HEAVY, ref _heavy);

        public ObservableAttribute Agility => GetAttribute(AttributeNames.AGILITY, ref _agility);
        public ObservableAttribute Cunning => GetAttribute(AttributeNames.CUNNING, ref _cunning);
        public ObservableAttribute Spirit => GetAttribute(AttributeNames.SPIRIT, ref _spirit);
        public ObservableAttribute Strength => GetAttribute(AttributeNames.STRENGTH, ref _cunning);
        public ObservableAttribute Lore => GetAttribute(AttributeNames.LORE, ref _lore);
        public ObservableAttribute Luck => GetAttribute(AttributeNames.LUCK, ref _luck);

        public ObservableAttribute Combat => GetAttribute(AttributeNames.COMBAT, ref _combat);
        public ObservableAttribute Range => GetAttribute(AttributeNames.RANGE, ref _range);
        public ObservableAttribute Initiative => GetAttribute(AttributeNames.INITIATIVE, ref _iniative);
        public ObservableAttribute Melee => GetAttribute(AttributeNames.MELEE, ref _melee);

        public ObservableAttribute Wounds => GetAttribute(AttributeNames.WOUNDS, ref _wounds);
        public ObservableAttribute Health => GetAttribute(AttributeNames.HEALTH, ref _health);
        public ObservableAttribute Horror => GetAttribute(AttributeNames.HORROR, ref _horror);
        public ObservableAttribute Sanity => GetAttribute(AttributeNames.SANITY, ref _sanity);
        public ObservableAttribute Defence => GetAttribute(AttributeNames.DEFENCE, ref _defence);
        public ObservableAttribute Willpower => GetAttribute(AttributeNames.WILLPOWER, ref _willpower);

        public ObservableAttribute Dollars => GetAttribute(AttributeNames.DOLLARS, ref _dollars);
        public ObservableAttribute DarkStone => GetAttribute(AttributeNames.DARKSTONE, ref _darkStone);

        #endregion Observable Attribute

        [RelayCommand]
        public async Task UpdateAttribute(ObservableAttribute attribute)
        {
            if (await _navigationService.PushAsync<UpdateAttributePopup, bool>(new Dictionary<string, object> { { nameof(Attribute), attribute } }))
            {
                await _characterService.SaveAsync(_character);
            }
        }

        [RelayCommand]
        public async Task IncrementAttribute(ObservableAttribute attribute)
        {
            if (await _navigationService.PushAsync<IncrementAttributePopup, bool>(new Dictionary<string, object> { { nameof(Attribute), attribute } }))
            {
                await _characterService.SaveAsync(_character);
            }
        }

        [RelayCommand]
        public async Task DeleteCharacter()
        {
            if (await _navigationService.DisplayAlert("Are you sure?", "This will delete the character and all progress.", "Yes", "No"))
            {
                _characterService.Delete(Character);
                await _navigationService.NavigateBackAsync();
            }
        }

        private ObservableAttribute GetAttribute(string name, ref ObservableAttribute attribute)
        {
            if (Character == null)
            {
                return attribute;
            }
            else if (!Character.Attributes.ContainsKey(name))
            {
                Character.AddAttribute(name, attribute);
            }
            else if (attribute != Character.Attributes[name])
            {
                attribute = Character.Attributes[name];
            }
            return attribute;
        }
    }
}