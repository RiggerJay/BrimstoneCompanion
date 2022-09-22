namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    public class Feature
    {
        public string Name { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public int Quantity { get; set; } = 1;

        public IDictionary<string, int> Properties { get; set; }
            = new Dictionary<string, int>();

        public int? Value { get; set; }

        public int Weight { get; set; }

        public FeatureTypes FeatureType { get; set; } = FeatureTypes.Gear;

        public IList<Keyword> Keywords { get; set; } = new List<Keyword>();

        public bool NextAdventure { get; set; } = false;
    }
}