namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    public class Template
    {
        public IDictionary<string, AttributeValue> Attributes { get; set; }
            = new Dictionary<string, AttributeValue>();
    }
}
