using RedSpartan.BrimstoneCompanion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableFeature : ObservableModel<Feature>
    {
        public ObservableFeature(Feature model) : base(model)
        {
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

        public int Value
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

        public IDictionary<string, int> Properties => Model.Properties;
    }
}