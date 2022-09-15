using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IRepository<Character> _repository;
        private readonly ObservableCollection<ObservableCharacter> _characters = new();

        public CharacterService(IRepository<Character> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public bool Delete(ObservableCharacter character)
        {
            if (_repository.Delete(character.Id))
            {
                _characters.Remove(character);
                return true;
            }
            return false;
        }

        public async Task<ObservableCollection<ObservableCharacter>> GetAllAsync()
        {
            if (_characters.Count == 0)
            {
                foreach (var character in await _repository.GetAsync())
                {
                    _characters.Add(ObservableCharacter.New(character));
                }
            }
            return _characters;
        }

        public async Task SaveAsync(ObservableCharacter character)
        {
            await _repository.SaveAsync(character.GetModel(), character.Id);
        }

        public Task<ObservableCharacter> NewAsync(string name, string role)
        {
            var character = new ObservableCharacter
            {
                Name = name,
                Class = role,
            };

            SetAttributes(character);

            return Task.FromResult(character);
        }

        private void SetAttributes(ObservableCharacter character)
        {
            character.SetAttribute(AttributeNames.XP, 0);
            character.SetAttribute(AttributeNames.GRIT, 1, 2);
            character.SetAttribute(AttributeNames.CORRUPTION, 0, 5);
            character.SetAttribute(AttributeNames.HEAVY, 0, 5);
            character.SetAttribute(AttributeNames.AGILITY, 2);
            character.SetAttribute(AttributeNames.CUNNING, 2);
            character.SetAttribute(AttributeNames.SPIRIT, 2);
            character.SetAttribute(AttributeNames.STRENGTH, 2);
            character.SetAttribute(AttributeNames.LORE, 2);
            character.SetAttribute(AttributeNames.LUCK, 2);
            character.SetAttribute(AttributeNames.COMBAT, 2);
            character.SetAttribute(AttributeNames.INITIATIVE, 8);
            character.SetAttribute(AttributeNames.MELEE, 4);
            character.SetAttribute(AttributeNames.RANGE, 4);
            character.SetAttribute(AttributeNames.WOUNDS, 0, 10);
            character.SetAttribute(AttributeNames.HEALTH, 10, 10);
            character.SetAttribute(AttributeNames.HORROR, 2);
            character.SetAttribute(AttributeNames.SANITY, 10, 10);
            character.SetAttribute(AttributeNames.DEFENCE, 4);
            character.SetAttribute(AttributeNames.WILLPOWER, 4);
            character.SetAttribute(AttributeNames.DOLLARS, 0);
            character.SetAttribute(AttributeNames.DARKSTONE, 0);
        }
    }
}