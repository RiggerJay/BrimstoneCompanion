using System.ComponentModel;

namespace RedSpartan.BrimstoneCompanion.Domain.Models
{
    public class Token
    {
        [DefaultValue("")]
        public string Name { get; set; } = string.Empty;

        [DefaultValue("")]
        public string Description { get; set; } = string.Empty;
    }
}