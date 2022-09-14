namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    public class Note
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
