using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Newtonsoft.Json;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class ExportViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private ObservableCharacter _character;

        [ObservableProperty]
        private string _export;

        public ExportViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ObservableCharacter Character
        {
            get => _character;
            set
            {
                if (SetProperty(ref _character, value))
                {
                    Export = JsonConvert.SerializeObject(_character.GetModel(), JsonConstants.Settings);
                }
            }
        }

        [RelayCommand]
        public async Task Close()
        {
            await _mediator.Send(NavRequest.Close(true));
        }
    }
}