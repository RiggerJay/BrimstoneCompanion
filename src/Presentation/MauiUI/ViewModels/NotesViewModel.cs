using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
            _state.PropertyChanged += State_PropertyChanged;
        }

        public ObservableCollection<ObservableNote> Notes => Character.Notes;

        public ObservableCharacter Character => _state.Character;

        public bool CharacterLoaded => _state.CharacterLoaded;

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

        [RelayCommand]
        private async Task ExportCharacter()
            => await _mediator.Send(NavRequest.ExportCharacter(_state.Character));

        [RelayCommand]
        public async Task ShowFeatures() => await _mediator.Send(NavRequest.ShowFeatures());

        [RelayCommand]
        public async Task ShowCharacter() => await _mediator.Send(NavRequest.ShowCharacter());

        private Task SaveCharacter() =>
            _mediator.Send(SaveCharacterRequest.Save());

        private void State_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Character")
            {
                OnPropertyChanged(nameof(Character));
                OnPropertyChanged(nameof(CharacterLoaded));
                OnPropertyChanged(nameof(Notes));
            }
        }
    }
}