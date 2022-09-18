using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface ICharacterService
    {
        Task SaveAsync(ObservableCharacter character);

        bool Delete(ObservableCharacter character);

        Task<ObservableCollection<ObservableCharacter>> GetAllAsync();

        Task Initialise();

        Task<ObservableCharacter> NewAsync(string name, string role);
    }
}