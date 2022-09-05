using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IRepository<Character> _repository;
        private readonly ObservableCollection<ObservableCharacter> _characters = new ObservableCollection<ObservableCharacter>();

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
                    _characters.Add(new ObservableCharacter(character));
                }
            }
            return _characters;
        }

        public async Task SaveAsync(ObservableCharacter character)
        {
            await _repository.SaveAsync(character.GetModel(), character.Id);
        }
    }
}