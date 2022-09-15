using RedSpartan.BrimstoneCompanion.Domain.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableFeature : ObservableModel<Feature>
    {
        private ObservableFeature(Feature model) : base(model)
        {
            foreach (var prop in Model.Properties)
            {
                Properties.Add(ObservableProp.New(prop.Key, prop.Value));
            }

            Properties.CollectionChanged += Properties_CollectionChanged;
        }

        public string Name
        {
            get => Model.Name;
            set => SetProperty(Model.Name, value, Model, (model, _value) => model.Name = _value);
        }

        public string Details
        {
            get => Model.Details;
            set => SetProperty(Model.Details, value, Model, (model, _value) => model.Details = _value);
        }

        public int Quantity
        {
            get => Model.Quantity;
            set => SetProperty(Model.Quantity, value, Model, (model, _value) => model.Quantity = _value);
        }

        public int? Value
        {
            get => Model.Value;
            set => SetProperty(Model.Value, value, Model, (model, _value) => model.Value = _value);
        }

        public FeatureTypes FeatureType
        {
            get => Model.FeatureType;
            set => SetProperty(Model.FeatureType, value, Model, (model, _value) => model.FeatureType = _value);
        }

        public string Keywords
        {
            get => Model.Keywords;
            set => SetProperty(Model.Keywords, value, Model, (model, _value) => model.Keywords = _value);
        }

        public bool NextAdventure
        {
            get => Model.NextAdventure;
            set => SetProperty(Model.NextAdventure, value, Model, (model, _value) => model.NextAdventure = _value);
        }

        public ObservableCollection<ObservableProp> Properties { get; } = new();

        public void PropertiesChanged() => OnPropertyChanged(nameof(Properties));

        private void Properties_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs args)
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
                        if (item is ObservableProp prop)
                        {
                            if (Model.Properties.ContainsKey(prop.Key))
                            {
                                Model.Properties[prop.Key] = prop.Value;
                            }
                            else
                            {
                                Model.Properties.Add(prop.Key, prop.Value);
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
                        if (item is ObservableProp prop
                            && Model.Properties.ContainsKey(prop.Key))
                        {
                            Model.Properties.Remove(prop.Key);
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Model.Properties.Clear();
                    break;
            }
        }

        public static ObservableFeature New() => new(new Feature());

        public static ObservableFeature New(Feature model) => new(model);

        public void AddProperty(string key, int value) =>
            Properties.Add(ObservableProp.New(key, value));
    }
}