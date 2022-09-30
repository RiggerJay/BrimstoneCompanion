using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface IUpdateApplicationState : INotifyPropertyChanged, INotifyPropertyChanging
    {
        Task<bool> SetCharacterAsync(ObservableCharacter character);
    }
}