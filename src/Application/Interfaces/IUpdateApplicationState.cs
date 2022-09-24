using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface IUpdateApplicationState
    {
        Task<bool> UpdateCharacterAsync(ObservableCharacter character);
    }
}