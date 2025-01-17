﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.Infrastructure.Requests;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class KeywordViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveAndCloseCommand))]
        private string _keyword;

        public KeywordViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        public async Task SaveAndClose()
        {
            await _mediator.Send(NavRequest.Close(Keyword));
        }

        private bool CanSave() => !string.IsNullOrWhiteSpace(Keyword);
    }
}