using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class NewNoteViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        public NewNoteViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ObservableNote Note { get; } = ObservableNote.New();

        public string NoteTitle
        {
            get => Note.Title;
            set => SetProperty(Note.Title, value, Note, (note, _value) => { note.Title = _value; }, SaveAndCloseCommand.NotifyCanExecuteChanged);
        }

        [RelayCommand(CanExecute = nameof(CanSaveAndClose))]
        public async Task SaveAndClose() =>
            await _mediator.Send(NavRequest.Close(Note));

        public bool CanSaveAndClose() =>
            !string.IsNullOrWhiteSpace(Note.Title);
    }
}