using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IRepository<Character> _repository;
        private readonly ITemplateService _templateCharacter;
        private readonly ObservableCollection<ObservableCharacter> _characters = new();

        public CharacterService(IRepository<Character> repository
            , ITemplateService templateCharacter)
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
            foreach (var character in await _repository.GetAsync())
            {
                try
                {
                    var observableCharacter = ObservableCharacter.New(character);
                    if (!_characters.Any(x => x.Id == observableCharacter.Id))
                    {
                        _characters.Add(observableCharacter);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
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

            await UpdateCharacterFromTemplate(character);

            return character;
        }

        public async Task UpdateCharacterFromTemplate(ObservableCharacter character)
        {
            var _updated = false;
            var template = await _templateCharacter.Get(character.Class);
            foreach (var attributeValue in template.Attributes)
            {
                if (!character.Attributes.Any(x => x.Key == attributeValue.Key))
                {
                    var attribute = ObservableAttribute.New(attributeValue.Key, attributeValue.Value.Value, attributeValue.Value.MaxValue, character.Features);
                    character.Attributes.Add(attribute);
                    _updated = true;
                }
            }

            if (_updated)
            {
                await SaveAsync(character);
            }
        }
    }
}