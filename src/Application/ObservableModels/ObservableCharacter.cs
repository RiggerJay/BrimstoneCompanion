using RedSpartan.BrimstoneCompanion.Domain.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableCharacter : ObservableModel<Character>
    {
        public ObservableCharacter() : this(new Character())
        { }

        public ObservableCharacter(Character character) : base(character)
        {
            foreach (var attribute in Model.Attributes)
            {
                Attributes.Add(attribute.Key, new ObservableAttribute(attribute.Key, attribute.Value));
            }

            foreach (var feature in Model.Features)
            {
                Features.Add(new ObservableFeature(feature));
            }

            Features.CollectionChanged += Features_CollectionChanged;
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

        public IDictionary<string, ObservableAttribute> Attributes { get; } = new Dictionary<string, ObservableAttribute>();

        public ObservableCollection<ObservableFeature> Features { get; set; } = new ObservableCollection<ObservableFeature>();

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
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    if (args.NewItems == null)
                    {
                        return;
                    }
                    foreach (var item in args.NewItems)
                    {
                        if (item is ObservableFeature feature)
                        {
                            Model.Features.Remove(feature.GetModel());
                        }
                    }
                    break;
            }
        }

        public ObservableAttribute GetAttribute(string name)
        {
            if (!Attributes.ContainsKey(name))
            {
                ObservableAttribute attribute = ObservableAttribute.New(name, 0);
                Attributes.Add(name, attribute);
                Model.Attributes.Add(name, attribute.GetModel());
            }

            return Attributes[name];
        }

        public int GetCalculatedValue(ObservableAttribute attribute) =>
            attribute.Value + Features.SelectMany(x => x.Properties.Where(p => p.Key == attribute.Key).Select(p => p.Value)).Sum();
    }
}