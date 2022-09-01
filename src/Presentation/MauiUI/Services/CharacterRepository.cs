using RedSpartan.BrimstoneCompanion.Domain.Models;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Services
{
    public class CharacterRepository : BaseRepository<Character>
    {
        public CharacterRepository(IFileSystem fileSystem) : base(fileSystem)
        { }
    }
}
