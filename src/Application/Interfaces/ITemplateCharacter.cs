using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface ITemplateCharacter
    {
        Task<Template> Get(string role);
    }
}
