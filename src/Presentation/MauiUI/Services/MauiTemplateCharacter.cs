using Newtonsoft.Json;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Services
{
    public class MauiTemplateCharacter : ITemplateCharacter
    {
        public async Task<Template> Get(string role)
        {
            var json = await LoadRoleAsset("default");
            return JsonConvert.DeserializeObject<Template>(json);
        }

        private async Task<string> LoadRoleAsset(string role)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("default.json");
                using var reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }
}
