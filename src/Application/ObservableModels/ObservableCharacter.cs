using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableCharacter : ObservableModel<Character>
    {
        public ObservableCharacter(string name, string role) : this(new Character { Name = name, Class = role })
        { }

        private ObservableCharacter(Character character) : base(character)
        {
            foreach (var attribute in Model.Attributes)
            {
                Attributes.Add(attribute.Key, ObservableAttribute.New(this, attribute.Key, attribute.Value));
            }

            foreach (var feature in Model.Features)
            {
                Features.Add(ObservableFeature.New(feature));
            }

            Features.CollectionChanged += Features_CollectionChanged;

            foreach (var note in Model.Notes)
            {
                Notes.Add(ObservableNote.New(note));
            }

            Notes.CollectionChanged += Notes_CollectionChanged;

            if (Model.Keywords == null)
            {
                Model.Keywords = new List<Keyword>();
            }

            foreach (var keyword in Model.Keywords)
            {
                Keywords.Add(ObservableKeyword.New(keyword));
            }

            Keywords.CollectionChanged += Keywords_CollectionChanged; ;
        }

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

        public int CurrentWeight => GetAttribute(AttributeNames.HEAVY).Value + Features.Sum(x => x.Weight);

        public ObservableCollection<ObservableKeyword> Keywords { get; } = new ObservableCollection<ObservableKeyword>();

        public IDictionary<string, ObservableAttribute> Attributes { get; } = new Dictionary<string, ObservableAttribute>();

        public ObservableCollection<ObservableFeature> Features { get; set; } = new ObservableCollection<ObservableFeature>();

        public IList<ObservableKeyword> ConcatKeywords => Keywords.Concat(Features.SelectMany(x => x.Keywords)).ToList();

        public ObservableCollection<ObservableNote> Notes { get; set; } = new ObservableCollection<ObservableNote>();

        public ObservableAttribute GetAttribute(string name)
        {
            SetAttribute(name, 0);

            return Attributes[name];
        }

        public void UpdateKeywords()
        {
            OnPropertyChanged(nameof(ConcatKeywords));
        }

        public void SetAttribute(string name, int value, int? maxValue = null)
        {
            if (!Attributes.ContainsKey(name))
            {
                ObservableAttribute attribute = ObservableAttribute.New(this, name, value, maxValue);
                Attributes.Add(name, attribute);
                Model.Attributes.Add(name, attribute.GetModel());
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

        private void Features_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
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
                        if (item is ObservableFeature feature)
                        {
                            Model.Features.Add(feature.GetModel());
                            foreach (var prop in feature.Properties)
                            {
                                ValueChanged(prop.Key);
                            }
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
                        if (item is ObservableFeature feature)
                        {
                            Model.Features.Remove(feature.GetModel());
                            foreach (var prop in feature.Properties)
                            {
                                ValueChanged(prop.Key);
                            }
                        }
                    }
                    break;
            }
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

        internal static ObservableCharacter New(Character character) => new(character);

        internal static ObservableCharacter New() => new(new Character());
    }
}