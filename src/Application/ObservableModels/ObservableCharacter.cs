﻿using RedSpartan.BrimstoneCompanion.Domain.Models;
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
                Attributes.Add(attribute.Key, ObservableAttribute.New(this, attribute.Key, attribute.Value));
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
                            foreach (var prop in feature.Properties)
                            {
                                ValueChanged(prop.Key);
                            }
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
                            foreach (var prop in feature.Properties)
                            {
                                ValueChanged(prop.Key);
                            }
                        }
                    }
                    break;
            }
        }

        public ObservableAttribute GetAttribute(string name)
        {
            SetAttribute(name, 0);

            return Attributes[name];
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
            }
        }
    }
}