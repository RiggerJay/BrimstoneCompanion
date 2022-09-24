using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Services
{
    public class CharacterService : ICharacterService
    {
        private bool _initialising = false;
        private bool _initialised = false;
        private readonly IRepository<Character> _repository;
        private readonly ITemplateCharacter _templateCharacter;
        private readonly ObservableCollection<ObservableCharacter> _characters = new();

        public CharacterService(IRepository<Character> repository
            , ITemplateCharacter templateCharacter)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _templateCharacter = templateCharacter ?? throw new ArgumentNullException(nameof(templateCharacter));
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

        public async Task<ObservableCharacter> CreateAsync(string name, string role)
        {
            var character = new ObservableCharacter(name, role);

            UpdateCharacter(character, await _templateCharacter.Get(role));

            return character;
        }

        private static void UpdateCharacter(ObservableCharacter character, Template template)
        {
            foreach (var attribute in template.Attributes)
            {
                if (!character.Attributes.ContainsKey(attribute.Key))
                {
                    character.Attributes.Add(attribute.Key, ObservableAttribute.New(character, attribute.Key, attribute.Value));
                }
            }
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

        public async Task<bool> IsValid(ObservableCharacter character)
        {
            var template = await _templateCharacter.Get(character.Class);

            foreach (var attribute in template.Attributes)
            {
                if (!character.Attributes.ContainsKey(attribute.Key))
                {
                    return false;
                }
            }

            return true;
        }
    }
}