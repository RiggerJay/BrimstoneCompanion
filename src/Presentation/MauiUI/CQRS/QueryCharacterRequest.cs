using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.CQRS
{
    public class QueryCharacterRequest : IRequest<ObservableCollection<ObservableCharacter>>
    {
        private QueryCharacterRequest()
        { }

        public static QueryCharacterRequest All() => new();
    }
}