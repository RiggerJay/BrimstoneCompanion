using Newtonsoft.Json;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Services
{
    public class BaseRepository<T> : IRepository<T>
    {
        private readonly IFileSystem _fileSystem;

        public BaseRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public virtual async Task<T> GetAsync(string key)
        {
            using StreamReader reader = new(GetFilePath(key));

            return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync());
        }

        public virtual async Task<IList<T>> GetAsync()
        {
            var list = new List<T>();

            foreach (var file in Directory.GetFiles(GetFolderName()))
            {
                using StreamReader reader = new(file);
                
                list.Add(JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync()));
            }

            return list;
        }

        public virtual async Task SaveAsync(T model, string key)
        {
            var json = JsonConvert.SerializeObject(model);

            using StreamWriter writer = new(GetFilePath(key), false);

            await writer.WriteAsync(json);
        }

        protected string GetFilePath(string key)
        {
            return Path.Combine(GetFolderName(), $"{key}.json");
        }

        protected string GetFolderName()
        {
            return Path.Combine(_fileSystem.AppDataDirectory, typeof(T).Name);
        }
    }
}
