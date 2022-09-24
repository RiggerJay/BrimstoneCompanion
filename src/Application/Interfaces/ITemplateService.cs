using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface ITemplateService
    {
        Task<Template> Get(string role);
    }
}
