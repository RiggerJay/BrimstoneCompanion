using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class CharacterSelectorViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;

        [ObservableProperty]
        private ObservableCharacter? _selectedCharacter;

        public CharacterSelectorViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            Title = "Character Selector";
        }

        [RelayCommand]
        private async Task NewCharacter()
        {
            var results = await _mediator.Send(NavRequest.CreateCharacter());

            if (results != null)
            {
                Characters.Add(results);
                SelectedCharacter = results;
                await _mediator.Send(SaveCharacterRequest.Save());
            }
        }

        [RelayCommand]
        private async Task ImportCharacter()
        {
            var results = await _mediator.Send(NavRequest.ImportCharacter());

            if (results != null)
            {
                Characters.Add(results);
                SelectedCharacter = results;
                await _mediator.Send(SaveCharacterRequest.Save());
            }
        }

        public ObservableCollection<ObservableCharacter> Characters { get; set; } = new();

        [RelayCommand]
        public async Task Initialise()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                await _mediator.Send(InitialiseRequest.Go());
                Characters = await _mediator.Send(QueryCharacterRequest.All());
                OnPropertyChanged(nameof(Characters));
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.PropertyName == nameof(SelectedCharacter)
                && SelectedCharacter != null)
            {
                await _mediator.Send(LoadCharacterRequest.With(SelectedCharacter));
                SelectedCharacter = null;
            }
        }
    }
}