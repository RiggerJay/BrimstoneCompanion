namespace RedSpartan.BrimstoneCompanion.Infrastructure.Messages
{
    public class CharacterLoaded
    {
        public bool Success { get; }
        private CharacterLoaded(bool success)
        {
            Success = success;
        }

        public static CharacterLoaded Successful() => new(true);
    }
}
