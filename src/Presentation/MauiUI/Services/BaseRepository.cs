using Newtonsoft.Json;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using System.Text;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Services
{
    public class BaseRepository<T> : IRepository<T>
    {
        private readonly IFileSystem _fileSystem;

        public BaseRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        }

        public virtual async Task<IList<T>> GetAsync()
        {
            var list = new List<T>();

            foreach (var file in Directory.GetFiles(GetFolderName()))
            {
                list.Add(JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(file, Encoding.UTF8)));
            }

            return list;
        }

        public virtual async Task SaveAsync(T model, string key)
        {
            var json = JsonConvert.SerializeObject(model);

            await File.WriteAllTextAsync(GetFilePath(key), json, Encoding.UTF8);
        }

        protected string GetFilePath(string key)
        {
            return Path.Combine(GetFolderName(), $"{key}.json");
        }

        protected string GetFolderName()
        {
            var path = Path.Combine(_fileSystem.AppDataDirectory, typeof(T).Name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}