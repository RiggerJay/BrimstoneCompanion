using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    [Serializable]
    public class Feature
    {
        [DefaultValue("")]
        public string Name { get; set; } = string.Empty;

        [DefaultValue("")]
        public string Details { get; set; } = string.Empty;

        [DefaultValue(1)]
        public int Quantity { get; set; } = 1;

        public IDictionary<string, int> Properties { get; set; }
            = new Dictionary<string, int>();

        [DefaultValue(null)]
        public int? Value { get; set; }

        [DefaultValue(0)]
        public int Weight { get; set; }

        public FeatureTypes FeatureType { get; set; } = FeatureTypes.Gear;

        public IList<Keyword> Keywords { get; set; } = new List<Keyword>();

        [DefaultValue(false)]
        public bool NextAdventure { get; set; } = false;
    }
}