using RedSpartan.BrimstoneCompanion.Domain;
using RedSpartan.BrimstoneCompanion.Domain.Models;
using System;
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
                var obAtt = new ObservableAttribute(attribute.Key, attribute.Value);
                obAtt.CurrentValue += Model.Features.SelectMany(x => x.Properties.Where(p => p.Key == attribute.Key).Select(p => p.Value)).Sum();
                Attributes.Add(attribute.Key, obAtt);
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

        #region Observable Attribute

        public ObservableAttribute Experience => GetAttribute(AttributeNames.XP);
        public ObservableAttribute Grit => GetAttribute(AttributeNames.GRIT);
        public ObservableAttribute Corruption => GetAttribute(AttributeNames.CORRUPTION);
        public ObservableAttribute Heavy => GetAttribute(AttributeNames.HEAVY);

        public ObservableAttribute Agility => GetAttribute(AttributeNames.AGILITY);
        public ObservableAttribute Cunning => GetAttribute(AttributeNames.CUNNING);
        public ObservableAttribute Spirit => GetAttribute(AttributeNames.SPIRIT);
        public ObservableAttribute Strength => GetAttribute(AttributeNames.STRENGTH);
        public ObservableAttribute Lore => GetAttribute(AttributeNames.LORE);
        public ObservableAttribute Luck => GetAttribute(AttributeNames.LUCK);

        public ObservableAttribute Combat => GetAttribute(AttributeNames.COMBAT);
        public ObservableAttribute Range => GetAttribute(AttributeNames.RANGE);
        public ObservableAttribute Initiative => GetAttribute(AttributeNames.INITIATIVE);
        public ObservableAttribute Melee => GetAttribute(AttributeNames.MELEE);

        public ObservableAttribute Wounds => GetAttribute(AttributeNames.WOUNDS);
        public ObservableAttribute Health => GetAttribute(AttributeNames.HEALTH);
        public ObservableAttribute Horror => GetAttribute(AttributeNames.HORROR);
        public ObservableAttribute Sanity => GetAttribute(AttributeNames.SANITY);
        public ObservableAttribute Defence => GetAttribute(AttributeNames.DEFENCE);
        public ObservableAttribute Willpower => GetAttribute(AttributeNames.WILLPOWER);

        public ObservableAttribute Dollars => GetAttribute(AttributeNames.DOLLARS);
        public ObservableAttribute DarkStone => GetAttribute(AttributeNames.DARKSTONE);

        #endregion Observable Attribute

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

        private ObservableAttribute GetAttribute(string name)
        {
            if (Attributes.ContainsKey(name))
            {
                return Attributes[name];
            }

            ObservableAttribute attribute = ObservableAttribute.New(name, 0);
            Attributes.Add(name, attribute);
            Model.Attributes.Add(name, attribute.GetModel());
            return attribute;
        }

        public void AttributesChanged()
        {
            OnPropertyChanged(nameof(Experience));
            OnPropertyChanged(nameof(Grit));
            OnPropertyChanged(nameof(Corruption));
            OnPropertyChanged(nameof(Heavy));

            OnPropertyChanged(nameof(Agility));
            OnPropertyChanged(nameof(Cunning));
            OnPropertyChanged(nameof(Spirit));
            OnPropertyChanged(nameof(Strength));
            OnPropertyChanged(nameof(Lore));
            OnPropertyChanged(nameof(Luck));

            OnPropertyChanged(nameof(Combat));
            OnPropertyChanged(nameof(Range));
            OnPropertyChanged(nameof(Initiative));
            OnPropertyChanged(nameof(Melee));

            OnPropertyChanged(nameof(Wounds));
            OnPropertyChanged(nameof(Health));
            OnPropertyChanged(nameof(Horror));
            OnPropertyChanged(nameof(Sanity));
            OnPropertyChanged(nameof(Defence));
            OnPropertyChanged(nameof(Willpower));

            OnPropertyChanged(nameof(Dollars));
            OnPropertyChanged(nameof(DarkStone));
        }
    }
}