using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.AppLayer.Resources;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Services
{
    public class TextResourceService : ITextResource
    {
        public string GetValue(string key)
            => TextResource.ResourceManager.GetString(key) ?? string.Empty;
    }
}