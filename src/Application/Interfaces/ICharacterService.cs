using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface ICharacterService
    {
        Task SaveAsync(ObservableCharacter character);

        Task<bool> DeleteAsync(ObservableCharacter character);

        Task<ObservableCollection<ObservableCharacter>> GetAllAsync();

        Task<ObservableCharacter> CreateAsync(string name, string role);

        Task UpdateCharacterFromTemplate(ObservableCharacter character);
    }
}