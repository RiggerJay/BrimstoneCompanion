using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.Popups;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Character), nameof(Character))]
    public partial class CharacterViewModel : ViewModelBase
    {
        private readonly IRepository<Character> _repository;
        private readonly INavigationService _navigationService;

        #region Fields

        private ObservableCharacter _character;
        private ObservableAttribute _experience = ObservableAttribute.New(0);
        private ObservableAttribute _grit = ObservableAttribute.New(1, 2);
        private ObservableAttribute _corruption = ObservableAttribute.New(0, 5);
        private ObservableAttribute _heavy = ObservableAttribute.New(5);

        private ObservableAttribute _agility = ObservableAttribute.New(2);
        private ObservableAttribute _cunning = ObservableAttribute.New(2);
        private ObservableAttribute _spirit = ObservableAttribute.New(2);
        private ObservableAttribute _strength = ObservableAttribute.New(2);
        private ObservableAttribute _lore = ObservableAttribute.New(2);
        private ObservableAttribute _luck = ObservableAttribute.New(2);

        private ObservableAttribute _combat = ObservableAttribute.New(2);
        private ObservableAttribute _range = ObservableAttribute.New(4);
        private ObservableAttribute _iniative = ObservableAttribute.New(5);
        private ObservableAttribute _melee = ObservableAttribute.New(4);

        private ObservableAttribute _wounds = ObservableAttribute.New(0);
        private ObservableAttribute _health = ObservableAttribute.New(10);
        private ObservableAttribute _horror = ObservableAttribute.New(0);
        private ObservableAttribute _sanity = ObservableAttribute.New(10);
        private ObservableAttribute _defence = ObservableAttribute.New(4);
        private ObservableAttribute _willpower = ObservableAttribute.New(4);

        private ObservableAttribute _dollars = ObservableAttribute.New(0);
        private ObservableAttribute _darkStone = ObservableAttribute.New(0);

        #endregion Fields

        public CharacterViewModel(INavigationService navigationService, IRepository<Character> repository)
        {
            Title = "Character";
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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

                Task.Run(async () => await _repository.SaveAsync(_character.GetModel(), _character.Id));
            }
        }

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

        [RelayCommand]
        public async Task UpdateAttribute(ObservableAttribute attribute)
        {
            if (await _navigationService.PushAsync<UpdateAttributePopup, bool>(new Dictionary<string, object> { { nameof(Attribute), attribute } }))
            {
                await _repository.SaveAsync(_character.GetModel(), _character.Id);
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