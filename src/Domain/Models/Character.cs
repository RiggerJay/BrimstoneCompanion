using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    [Serializable]
    public class Character
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [DefaultValue("")]
        public string Name { get; set; } = string.Empty;

        [DefaultValue("")]
        public string Class { get; set; } = string.Empty;

        public IList<Keyword> Keywords { get; set; } = new List<Keyword>();

        [DefaultValue((byte)1)]
        public byte Level { get; set; } = 1;

        public IDictionary<string, AttributeValue> Attributes { get; set; }
            = new Dictionary<string, AttributeValue>();

        public IList<Feature> Features { get; set; } = new List<Feature>();

        public IList<Note> Notes { get; set; } = new List<Note>();
    }
}