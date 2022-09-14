using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class EditNoteViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        private ObservableNote _note = ObservableNote.New();
        private string _title = string.Empty;
        private string _body = string.Empty;

        public EditNoteViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ObservableNote Note
        {
            get => _note;
            set => SetProperty(ref _note, value, NoteUpdate);
        }

        public string NoteTitle
        {
            get => Note.Title;
            set => SetProperty(Note.Title, value, Note, (note, _value) => { note.Title = _value; }, SaveAndCloseCommand.NotifyCanExecuteChanged);
        }

        [RelayCommand(CanExecute = nameof(CanSaveAndClose))]
        public async Task SaveAndClose() =>
            await _mediator.Send(NavRequest.Close(true));

        public bool CanSaveAndClose() =>
            !string.IsNullOrWhiteSpace(Note.Title);

        public void Reset()
        {
            Note.Title = _title;
            Note.Body = _body;
        }

        public void NoteUpdate()
        {
            _title = Note.Title;
            _body = Note.Body;
            OnPropertyChanged(nameof(NoteTitle));
            SaveAndCloseCommand.NotifyCanExecuteChanged();
        }
    }
}