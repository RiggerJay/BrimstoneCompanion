using CommunityToolkit.Mvvm.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableProp : ObservableObject
    {
        private string _key;
        private int _value;

        private ObservableProp(string key, int value)
        {
            Key = string.IsNullOrWhiteSpace(key) ? throw new ArgumentNullException(nameof(key)) : key;
            Value = value;
        }

        public string Key
        {
            get => _key;
            set => SetProperty(ref _key, value);
        }

        public int Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public static ObservableProp New(string key, int value) => new(key, value);
    }
}