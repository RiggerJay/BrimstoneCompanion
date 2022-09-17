using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class NotesViewModel : ViewModelBase
    {
        private readonly IApplicationState _state;
        private readonly IMediator _mediator;

        public NotesViewModel(IMediator mediator, IApplicationState state)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _state = state ?? throw new ArgumentNullException(nameof(state));
        }

        public ObservableCollection<ObservableNote> Notes => Character.Notes;

        public ObservableCharacter Character => _state.Character;

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
        public async Task EditNote(ObservableNote note)
        {
            if (await _mediator.Send(NavRequest.EditNote(note)))
            {
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