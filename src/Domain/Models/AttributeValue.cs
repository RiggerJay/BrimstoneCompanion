using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    [Serializable]
    public class AttributeValue
    {
        [DefaultValue(0)]
        public int Value { get; set; }

        [DefaultValue(null)]
        public int? MaxValue { get; set; } = null;
    }
}