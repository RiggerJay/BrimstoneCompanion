﻿using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Character), nameof(Character))]
    public partial class CharacterViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        private ObservableCharacter _character;

        public CharacterViewModel(IMediator mediator)
        {
            Title = "Character";
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ObservableCharacter Character
        {
            get => _character;
            set
            {
                if (SetProperty(ref _character, value))
                {
                    Title = $"{_character.Name} a level {_character.Level} {_character.Class}";
                    AttributesChanged();
                }

                Task.Run(async () => await _mediator.Send(SaveCharacterRequest.WithCharacter(_character)));
            }
        }

        #region Observable Attribute

        public ObservableAttribute Experience => GetAttribute(AttributeNames.XP);
        public ObservableAttribute Grit => GetAttribute(AttributeNames.GRIT);
        public ObservableAttribute Corruption => GetAttribute(AttributeNames.CORRUPTION);
        public ObservableAttribute Heavy => GetAttribute(AttributeNames.HEAVY);

        public ObservableAttribute Agility => GetAttribute(AttributeNames.AGILITY);
        public ObservableAttribute Cunning => GetAttribute(AttributeNames.CUNNING);
        public ObservableAttribute Spirit => GetAttribute(AttributeNames.SPIRIT);
        public ObservableAttribute Strength => GetAttribute(AttributeNames.STRENGTH);
        public ObservableAttribute Lore => GetAttribute(AttributeNames.LORE);
        public ObservableAttribute Luck => GetAttribute(AttributeNames.LUCK);

        public ObservableAttribute Combat => GetAttribute(AttributeNames.COMBAT);
        public ObservableAttribute Range => GetAttribute(AttributeNames.RANGE);
        public ObservableAttribute Initiative => GetAttribute(AttributeNames.INITIATIVE);
        public ObservableAttribute Melee => GetAttribute(AttributeNames.MELEE);

        public ObservableAttribute Wounds => GetAttribute(AttributeNames.WOUNDS);
        public ObservableAttribute Health => GetAttribute(AttributeNames.HEALTH);
        public ObservableAttribute Horror => GetAttribute(AttributeNames.HORROR);
        public ObservableAttribute Sanity => GetAttribute(AttributeNames.SANITY);
        public ObservableAttribute Defence => GetAttribute(AttributeNames.DEFENCE);
        public ObservableAttribute Willpower => GetAttribute(AttributeNames.WILLPOWER);

        public ObservableAttribute Dollars => GetAttribute(AttributeNames.DOLLARS);
        public ObservableAttribute DarkStone => GetAttribute(AttributeNames.DARKSTONE);

        #endregion Observable Attribute

        [RelayCommand]
        public async Task UpdateAttribute(ObservableAttribute attribute)
        {
            if (await _mediator.Send(NavRequest.UpdateAttribute(attribute)))
            {
                await _mediator.Send(SaveCharacterRequest.WithCharacter(Character));
            }
        }

        [RelayCommand]
        public async Task IncrementAttribute(ObservableAttribute attribute)
        {
            if (await _mediator.Send(NavRequest.IncrementAttribute(attribute)))
            {
                await _mediator.Send(SaveCharacterRequest.WithCharacter(Character));
            }
        }

        [RelayCommand]
        public async Task DeleteCharacter()
        {
            if (await _mediator.Send(BoolAlertRequest.WithTitleAndMessage("Are you sure?", "This will delete the character and all progress.")))
            {
                await _mediator.Send(DeleteCharacterRequest.WithCharacter(Character));
                await _mediator.Send(NavRequest.Close());
            }
        }

        [RelayCommand]
        public async Task ShowNotes()
        {
            try
            {
                await _mediator.Send(NavRequest.ShowNotes(Character));
            }
            catch (Exception ex)
            {
                await _mediator.Send(AlertRequest.WithTitleAndMessage("Error", ex.Message));
            }
        }

        public void AttributesChanged()
        {
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
        }

        private ObservableAttribute GetAttribute(string name)
        {
            if (Character == null)
            {
                return null;
            }

            return Character.GetAttribute(name);
        }
    }
}