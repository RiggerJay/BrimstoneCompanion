using CommunityToolkit.Mvvm.Input;
using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using RedSpartan.BrimstoneCompanion.MauiUI.CQRS;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public partial class NewFeatureViewModel : ViewModelBase
    {
        private readonly IMediator _mediator;
        private string? _selectedProperty;
        private int? _value;

        public NewFeatureViewModel(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public ObservableFeature Feature { get; } = ObservableFeature.New();

        public IList<string> Types => Enum.GetNames(typeof(FeatureTypes));

        public IList<string> Properties { get; } = AttributeNames.Strings;

        public string? SelectedProperty { get => _selectedProperty; set => SetProperty(ref _selectedProperty, value); }

        public int? Value { get => _value; set => SetProperty(ref _value, value); }

        [RelayCommand]
        public async Task SaveAndClose()
        {
            if (!string.IsNullOrEmpty(SelectedProperty)
                && (Value != null && Value != 0))
            {
                Feature.Properties.Add(SelectedProperty, (int)Value);
            }

            await _mediator.Send(NavRequest.Close(Feature));
        }
    }
}