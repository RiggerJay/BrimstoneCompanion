using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Infrastructure.Messages;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Character), nameof(Character))]
    public partial class CharacterViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationState _state;
        private readonly IMessenger _messenger;

        public CharacterViewModel(IMediator mediator
            , IApplicationState state
            , IMessenger messenger)
        {
            Title = "Character";
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _state = state ?? throw new ArgumentNullException(nameof(state));
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            _state.PropertyChanged += State_PropertyChanged;

            _messenger.Register<KeywordMessage>(this, KeywordMessageHandle);

            Keywords = new ObservableCollection<ObservableKeyword>();
            RefillKeywords();
        }

        public ObservableCharacter Character => _state.Character;

        public bool CharacterLoaded => _state.CharacterLoaded;

        public ObservableCollection<ObservableKeyword> Keywords { get; }

        #region Observable Attribute

        public ObservableAttribute EXP => GetAttribute(AttributeNames.XP);
        public ObservableAttribute GRT => GetAttribute(AttributeNames.GRIT);
        public ObservableAttribute CPT => GetAttribute(AttributeNames.CORRUPTION);
        public ObservableAttribute HVY => GetAttribute(AttributeNames.HEAVY);

        public ObservableAttribute AGI => GetAttribute(AttributeNames.AGILITY);
        public ObservableAttribute CNG => GetAttribute(AttributeNames.CUNNING);
        public ObservableAttribute SPT => GetAttribute(AttributeNames.SPIRIT);
        public ObservableAttribute STR => GetAttribute(AttributeNames.STRENGTH);
        public ObservableAttribute LOR => GetAttribute(AttributeNames.LORE);
        public ObservableAttribute LUK => GetAttribute(AttributeNames.LUCK);

        public ObservableAttribute CBT => GetAttribute(AttributeNames.COMBAT);
        public ObservableAttribute RNG => GetAttribute(AttributeNames.RANGE);
        public ObservableAttribute INT => GetAttribute(AttributeNames.INITIATIVE);
        public ObservableAttribute MLE => GetAttribute(AttributeNames.MELEE);

        public ObservableAttribute HLT => GetAttribute(AttributeNames.HEALTH);
        public ObservableAttribute SAN => GetAttribute(AttributeNames.SANITY);
        public ObservableAttribute DEF => GetAttribute(AttributeNames.DEFENCE);
        public ObservableAttribute WIL => GetAttribute(AttributeNames.WILLPOWER);

        public ObservableAttribute DLR => GetAttribute(AttributeNames.DOLLARS);
        public ObservableAttribute DKS => GetAttribute(AttributeNames.DARKSTONE);

        #endregion Observable Attribute

        [RelayCommand]
        public async Task UpdateAttribute(ObservableAttribute attribute)
        {
            if (await _mediator.Send(NavRequest.UpdateAttribute(attribute)))
            {
                await SaveCharacterAsync();
            }
        }

        [RelayCommand]
        public async Task IncrementAttribute(ObservableAttribute attribute)
        {
            if (await _mediator.Send(NavRequest.IncrementAttribute(attribute)))
            {
                await SaveCharacterAsync();
            }
        }

        [RelayCommand]
        public async Task DeleteCharacter()
        {
            if (await _mediator.Send(BoolAlertRequest.WithTitleAndMessage("Are you sure?", "This will delete the character and all progress.")))
            {
                await _mediator.Send(DeleteCharacterRequest.WithCharacter(Character));
                await _mediator.Send(NavRequest.CharacterSelector());
            }
        }

        [RelayCommand]
        public async Task LevelUp()
        {
            if (await _mediator.Send(NavRequest.LevelUp(Character)))
            {
                await SaveCharacterAsync();
            }
        }

        [RelayCommand]
        public async Task AddKeyword()
        {
            var keyword = await _mediator.Send(NavRequest.Keyword());
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var word = ObservableKeyword.New(keyword, true);
                Character.Keywords.Add(word);
                Keywords.Add(word);
                await SaveCharacterAsync();
            }
        }

        [RelayCommand]
        public async Task Selector()
        {
            await _mediator.Send(NavRequest.CharacterSelector());
        }

        [RelayCommand]
        public async Task DeleteKeyword(ObservableKeyword keyword)
        {
            if (await _mediator.Send(BoolAlertRequest.WithTitleAndMessage("Are you sure?", $"You are deleting keyword '{keyword.Word}'."))
                && Character.Keywords.Contains(keyword))
            {
                Character.Keywords.Remove(keyword);
                Keywords.Remove(keyword);
                await SaveCharacterAsync();
            }
        }

        [RelayCommand]
        public async Task ShowFeatures() => await _mediator.Send(NavRequest.ShowFeatures());

        [RelayCommand]
        public async Task ShowNotes() => await _mediator.Send(NavRequest.ShowNotes());

        [RelayCommand]
        public async Task ShowSidebag() => await _mediator.Send(NavRequest.ShowSidebag());

        private void KeywordMessageHandle(object recipient, KeywordMessage message)
        {
            if (message.AddedKeyword
                && !Keywords.Contains(message.Keyword))
            {
                Keywords.Add(message.Keyword);
            }
            else
            {
                Keywords.Remove(message.Keyword);
            }
        }

        private ObservableAttribute GetAttribute(string name)
        {
            if (Character == null)
            {
                return null;
            }
            return Character.Attributes.First(x => x.Key == name);
        }

        private Task SaveCharacterAsync() =>
            _mediator.Send(SaveCharacterRequest.Save());

        private void State_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Character")
            {
                OnPropertyChanged(nameof(Character));
                OnPropertyChanged(nameof(CharacterLoaded));
                AttributesChanged();
                RefillKeywords();
            }
        }

        private void RefillKeywords()
        {
            Keywords.Clear();
            foreach (var keyword in Character.Keywords)
            {
                Keywords.Add(keyword);
            }
            foreach (var keyword in Character.Features.SelectMany(x => x.Keywords))
            {
                Keywords.Add(keyword);
            }
        }

        private void AttributesChanged()
        {
            foreach (var name in AttributeNames.Strings)
            {
                OnPropertyChanged(name);
            }
        }
    }
}