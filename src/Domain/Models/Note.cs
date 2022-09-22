using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    [Serializable]
    public class Note
    {
        [DefaultValue("")]
        public string Title { get; set; } = string.Empty;

        [DefaultValue("")]
        public string Body { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}