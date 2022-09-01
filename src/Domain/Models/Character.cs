namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    public class Character
    {
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public byte Level { get; set; } = 1;
    }
}