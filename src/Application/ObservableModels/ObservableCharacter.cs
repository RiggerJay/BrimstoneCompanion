using CommunityToolkit.Mvvm.ComponentModel;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public partial class ObservableCharacter : ObservableModel<Character>
    {
        [ObservableProperty]
        private int _currentWeight;

        public ObservableCharacter(string name, string role)
            : this(new Character { Name = name, Class = role })
        { }

        private ObservableCharacter(Character character) : base(character)
        {
            InitialiseAttributes();

            InitialiseFeatures();

            InitialiseNotes();

            InitialiseKeywords();

            InitialiseTokens();
        }

        #region Properties

        public string Id
        {
            get => Model.Id;
            set => SetProperty(Model.Id, value, Model, (model, _value) => model.Id = _value);
        }

        public string Name
        {
            get => Model.Name;
            set => SetProperty(Model.Name, value, Model, (model, _value) => model.Name = _value);
        }

        public string Class
        {
            get => Model.Class;
            set => SetProperty(Model.Class, value, Model, (model, _value) => model.Class = _value);
        }

        public byte Level
        {
            get => Model.Level;
            set => SetProperty(Model.Level, value, Model, (model, _value) => model.Level = _value);
        }

        //TODO: set the weight
        //public int CurrentWeight => GetAttribute(AttributeNames.HEAVY).Value + Features.Sum(x => x.Weight);

        #endregion Properties

        #region Collections

        public ObservableCollection<ObservableKeyword> Keywords { get; } = new ObservableCollection<ObservableKeyword>();

        public ObservableCollection<ObservableToken> Tokens { get; } = new ObservableCollection<ObservableToken>();

        public IDictionary<string, ObservableAttribute> Attributes { get; } = new Dictionary<string, ObservableAttribute>();

        public ObservableCollection<ObservableFeature> Features { get; set; } = new ObservableCollection<ObservableFeature>();

        public IList<ObservableKeyword> ConcatKeywords => Keywords.Concat(Features.SelectMany(x => x.Keywords)).ToList();

        public ObservableCollection<ObservableNote> Notes { get; set; } = new ObservableCollection<ObservableNote>();

        #endregion Collections

        #region Methods

        /*public ObservableAttribute GetAttribute(string name)
        {
            AddAttribute(name, 0);

            return Attributes[name];
        }

        public void UpdateKeywords()
        {
            OnPropertyChanged(nameof(ConcatKeywords));
        }

        public void AddAttribute(string name, int value, int? maxValue = null)
        {
            AddAttribute(name, new AttributeValue { Value = value, MaxValue = maxValue });
        }

        public void AddAttribute(string name, AttributeValue attribute)
        {
            if (!Attributes.ContainsKey(name))
            {
                Attributes.Add(name, ObservableAttribute.New(name, attribute));
                Model.Attributes.Add(name, attribute);
            }
        }

        public void ValueChanged(string key)
        {
            if (Attributes.ContainsKey(key))
            {
                Attributes[key].OnValueChanged();
                if (key == AttributeNames.HEAVY)
                {
                    OnPropertyChanged(nameof(CurrentWeight));
                }
            }
        }

        public void WeightChanged() => OnPropertyChanged(nameof(CurrentWeight));

        public void AddNote(string note)
        {
            Notes.Add(ObservableNote.New(note));
        }

        public void UpdateMoney(int? value)
        {
            if (!value.HasValue)
            {
                return;
            }
            GetAttribute(AttributeNames.DOLLARS).Value += (int)value;

            ValueChanged(AttributeNames.DOLLARS);
        }
        */

        public static ObservableCharacter New(Character character) => new(character);

        public static ObservableCharacter New() => new(new Character());

        #endregion Methods

        #region Model Construction

        private void InitialiseAttributes()
        {
            if (Model.Attributes is null)
            {
                Model.Attributes = new Dictionary<string, AttributeValue>();
            }

            foreach (var attribute in Model.Attributes)
            {
                Attributes.Add(attribute.Key, ObservableAttribute.New(attribute.Key, attribute.Value));
            }
        }

        private void InitialiseFeatures()
        {
            if (Model.Features is null)
            {
                Model.Features = new List<Feature>();
            }

            foreach (var feature in Model.Features)
            {
                Features.Add(ObservableFeature.New(feature));
            }

            Features.CollectionChanged += Features_CollectionChanged;
        }

        private void InitialiseNotes()
        {
            if (Model.Notes is null)
            {
                Model.Notes = new List<Note>();
            }

            foreach (var note in Model.Notes)
            {
                Notes.Add(ObservableNote.New(note));
            }

            Notes.CollectionChanged += Notes_CollectionChanged;
        }

        private void InitialiseKeywords()
        {
            if (Model.Keywords is null)
            {
                Model.Keywords = new List<Keyword>();
            }

            foreach (var keyword in Model.Keywords)
            {
                Keywords.Add(ObservableKeyword.New(keyword));
            }

            Keywords.CollectionChanged += Keywords_CollectionChanged;
        }

        private void InitialiseTokens()
        {
            if (Model.Tokens is null)
            {
                Model.Tokens = new List<Token>();
            }

            foreach (var token in Model.Tokens)
            {
                Tokens.Add(ObservableToken.New(token));
            }

            Tokens.CollectionChanged += Tokens_CollectionChanged;
        }

        private void Features_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            SubscribeToCollection<Feature, ObservableModel<Feature>>(args, Model.Features);
        }

        private void Notes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            SubscribeToCollection<Note, ObservableModel<Note>>(args, Model.Notes);
        }

        private void Keywords_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            SubscribeToCollection<Keyword, ObservableModel<Keyword>>(args, Model.Keywords);
            OnPropertyChanged(nameof(ConcatKeywords));
        }

        private void Tokens_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
        {
            SubscribeToCollection<Token, ObservableModel<Token>>(args, Model.Tokens);
        }

        private static void SubscribeToCollection<T, TModel>(NotifyCollectionChangedEventArgs args, IList<T> list)
            where TModel : ObservableModel<T>
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (args.NewItems == null)
                    {
                        return;
                    }
                    foreach (var item in args.NewItems)
                    {
                        if (item is TModel model)
                        {
                            list.Add(model.GetModel());
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (args.OldItems == null)
                    {
                        return;
                    }
                    foreach (var item in args.OldItems)
                    {
                        if (item is TModel model)
                        {
                            list.Remove(model.GetModel());
                        }
                    }
                    break;
            }
        }

        #endregion Model Construction
    }
}