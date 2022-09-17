using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface IApplicationState : INotifyPropertyChanged, INotifyPropertyChanging
    {
        ObservableCharacter Character { get; }
        bool CharacterLoaded { get; }
    }
}