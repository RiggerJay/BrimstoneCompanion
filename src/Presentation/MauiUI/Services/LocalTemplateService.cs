using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Services
{
    public class LocalTemplateService : ITemplateService
    {
        //private readonly ILogger _logger;
        public LocalTemplateService()
        {
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Template> Get(string role)
        {
            var json = await LoadRoleAsset("default");
            return JsonConvert.DeserializeObject<Template>(json) ?? new Template();
        }

        private async Task<string> LoadRoleAsset(string role)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync($"{role}.json");
                using var reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Failed to load role asset");
            }
            return "";
        }
    }
}
