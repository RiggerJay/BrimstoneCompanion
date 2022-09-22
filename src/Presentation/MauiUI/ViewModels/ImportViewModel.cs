using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Newtonsoft.Json;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class ImportViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveAndCloseCommand))]
        private string _import;

        public ImportViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [RelayCommand(CanExecute = nameof(CanSaveAndClose))]
        public async Task SaveAndClose()
        {
            var model = JsonConvert.DeserializeObject<Character>(Import, JsonConstants.Settings);
            var character = ObservableCharacter.New(model);

            await _mediator.Send(NavRequest.Close(character));
        }

        private bool CanSaveAndClose => !string.IsNullOrWhiteSpace(Import);
    }
}