using CommunityToolkit.Mvvm.Input;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Popups;

public partial class NewCharacterPopup : CommunityToolkit.Maui.Views.Popup
{
    private readonly ObservableCharacter _character;

    public NewCharacterPopup()
    {
        BindingContext = _character = new ObservableCharacter();
        CreateCommand = new RelayCommand(Create, CanCreate);

        _character.PropertyChanged += Character_PropertyChanged;
        InitializeComponent();
    }

    private void Character_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        CreateCommand.NotifyCanExecuteChanged();
    }

    public IList<string> Classes { get; } = new List<string>
    {
        "Assassin",
        "Bandito",
        "Cowboy",
        "Dark Stone Shaman",
        "Drifter",
        "Frontier Doc",
        "Gambler",
        "Gun Slinger",
        "Indian Scout",
        "Jargono Native",
        "Jargono Tribal Human",
        "Lawman",
        "Orphan",
        "Outlaw",
        "Preacher",
        "Prospector",
        "Rancher",
        "Saloon Girl",
        "Samurai Warrior",
        "Sorceress",
        "Traveling Monk",
        "U.S. Marshal",
        "Wandering Samurai"
    };

    public void SetFocus() => CharacterName.Focus();

    public RelayCommand CreateCommand { get; }

    public void Create()
    {
        _character.PropertyChanged -= Character_PropertyChanged;
        Close(_character);
    }

    public bool CanCreate()
    {
        return !string.IsNullOrWhiteSpace(_character.Name)
            && !string.IsNullOrWhiteSpace(_character.Class);
    }

    protected override void OnDismissedByTappingOutsideOfPopup()
    {
        _character.PropertyChanged -= Character_PropertyChanged;
        base.OnDismissedByTappingOutsideOfPopup();
    }
}