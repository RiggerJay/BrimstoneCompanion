namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    public class Feature
    {
        public string Name { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public IDictionary<string, int> Properties { get; set; }
            = new Dictionary<string, int>();

        public int Value { get; set; }

        public FeatureTypes FeatureType { get; set; } = FeatureTypes.Gear;

        public string Keywords { get; set; } = string.Empty;

        public bool NextAdventure { get; set; } = false;
    }
}