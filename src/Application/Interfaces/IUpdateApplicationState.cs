using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface IUpdateApplicationState
    {
        bool UpdateCharacter(ObservableCharacter character);
    }
}