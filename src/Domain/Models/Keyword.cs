using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    [Serializable]
    public class Keyword
    {
        [DefaultValue("")]
        public string Word { get; set; } = string.Empty;

        [DefaultValue(false)]
        public bool CanDelete { get; set; } = false;
    }
}