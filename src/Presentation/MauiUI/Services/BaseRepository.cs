using Newtonsoft.Json;
using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using RedSpartan.BrimstoneCompanion.Infrastructure;
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

        public bool Delete(string key)
        {
            var file = GetFilePath(key);
            if (File.Exists(file))
            {
                File.Delete(file);
                return true;
            }
            return false;
        }

        public virtual async Task<IList<T>> GetAsync()
        {
            var list = new List<T>();

            foreach (var file in Directory.GetFiles(GetFolderName()))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(file, Encoding.UTF8);
                    System.Diagnostics.Debug.WriteLine(json);
                    var model = JsonConvert.DeserializeObject<T>(json, JsonConstants.Settings);
                    list.Add(model);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return list;
        }

        public virtual async Task SaveAsync(T model, string key)
        {
            var json = JsonConvert.SerializeObject(model, JsonConstants.Settings);
            System.Diagnostics.Debug.Write(json);
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