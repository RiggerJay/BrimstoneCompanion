using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using System.Collections.ObjectModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    [QueryProperty(nameof(Character), nameof(Character))]
    public partial class CharacterNotesViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private ObservableCharacter _character;

        public CharacterNotesViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ObservableCharacter Character
        {
            get => _character;
            set => SetProperty(ref _character, value, OnCharacterUpdated);
        }

        public ObservableCollection<ObservableFeature> Features => Character.Features;

        [RelayCommand]
        private async Task AddFeature()
        {
            var feature = await _mediator.Send(NavRequest.CreateFeature());

            if (feature != null)
            {
                Features.Add(feature);
                await _mediator.Send(SaveCharacterRequest.WithCharacter(Character));
            }
        }

        private void OnCharacterUpdated()
        {
            OnPropertyChanged(nameof(Features));
        }
    }
}