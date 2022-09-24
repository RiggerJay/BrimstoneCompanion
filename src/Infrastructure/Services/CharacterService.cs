using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Services
{
    public class CharacterService : ICharacterService
    {
        private bool _initialising = false;
        private bool _initialised = false;
        private readonly IRepository<Character> _repository;
        private readonly ObservableCollection<ObservableCharacter> _characters = new();

        public CharacterService(IRepository<Character> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<bool> DeleteAsync(ObservableCharacter character)
        {
            if (_repository.Delete(character.Id))
            {
                _characters.Remove(character);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<ObservableCollection<ObservableCharacter>> GetAllAsync()
        {
            while (_initialising)
            {
                await Task.Delay(100);
            }
            if (_characters.Count == 0)
            {
                await InitialiseAsync();
            }
            return _characters;
        }

        public async Task SaveAsync(ObservableCharacter character)
        {
            await _repository.SaveAsync(character.GetModel(), character.Id);
        }

        public Task<ObservableCharacter> CreateAsync(string name, string role)
        {
            var character = new ObservableCharacter(name, role);

            SetAttributes(character);

            return Task.FromResult(character);
        }

        private static void SetAttributes(ObservableCharacter character)
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
            character.SetAttribute(AttributeNames.HEALTH, 10, 10);
            character.SetAttribute(AttributeNames.SANITY, 10, 10);
            character.SetAttribute(AttributeNames.DEFENCE, 4);
            character.SetAttribute(AttributeNames.WILLPOWER, 4);
            character.SetAttribute(AttributeNames.DOLLARS, 0);
            character.SetAttribute(AttributeNames.DARKSTONE, 0);
        }



        public async Task InitialiseAsync()
        {
            if (_initialised)
            {
                return;
            }
            _initialising = true;
            foreach (var character in await _repository.GetAsync())
            {
                try
                {
                    _characters.Add(ObservableCharacter.New(character));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            _initialising = false;
            _initialised = true;
        }

        public Task<bool> IsValid(ObservableCharacter character)
        {
            

            return Task.FromResult(true);
        }
    }
}