namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    [Serializable]
    public class FullCharacter : Character
    {
        public IDictionary<string, Attribute> Attributes { get; set; }
            = new Dictionary<string, Attribute>();
    }
}