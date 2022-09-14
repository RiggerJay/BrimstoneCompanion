using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Character), nameof(Character))]
    public partial class NotesViewModel : ViewModelBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Notes))]
        private ObservableCharacter _character;

        private readonly IMediator _mediator;

        public NotesViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ObservableCollection<ObservableNote> Notes => Character?.Notes;

        [RelayCommand]
        public async Task AddNote()
        {
            var note = await _mediator.Send(NavRequest.CreateNote());

            if (note != null)
            {
                Notes.Add(note);
                await SaveCharacter();
            }
        }

        [RelayCommand]
        private async Task DeleteNote(ObservableNote? note)
        {
            if (note != null
                && await _mediator.Send(BoolAlertRequest.WithTitleAndMessage("Are you sure?", "You will lose this Note for good.")))
            {
                Notes.Remove(note);
                await SaveCharacter();
            }
        }

        private Task SaveCharacter() =>
            _mediator.Send(SaveRequest<ObservableCharacter>.With(Character));
    }
}