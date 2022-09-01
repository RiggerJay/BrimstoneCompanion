namespace BrimstoneCompanion.Domain.Models
{
    public class Character
    {
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;

        public IDictionary<string, Attribute> Attributes { get; set; }
            = new Dictionary<string, Attribute>();
    }
}
