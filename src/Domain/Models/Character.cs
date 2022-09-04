namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    [Serializable]
    public class Character
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public byte Level { get; set; } = 1;

        public IDictionary<string, AttributeStat> Attributes { get; set; }
            = new Dictionary<string, AttributeStat>();
    }
}