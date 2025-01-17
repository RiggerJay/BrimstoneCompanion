﻿using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Handlers
{
    public class QueryCharacterHandler : IRequestHandler<QueryCharacterRequest, ObservableCollection<ObservableCharacter>>
    {
        private readonly ICharacterService _service;

        public QueryCharacterHandler(ICharacterService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<ObservableCollection<ObservableCharacter>> Handle(QueryCharacterRequest request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync();
        }
    }
}