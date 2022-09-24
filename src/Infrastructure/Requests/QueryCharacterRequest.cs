using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Requests
{
    public class QueryCharacterRequest : IRequest<ObservableCollection<ObservableCharacter>>
    {
        private QueryCharacterRequest()
        { }

        public static QueryCharacterRequest All() => new();
    }
}