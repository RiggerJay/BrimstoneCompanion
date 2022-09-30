namespace RedSpartan.BrimstoneCompanion.Infrastructure.Messages
{
    public class PropertyChangedMessage
    {
        public string Name { get; }

        private PropertyChangedMessage(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public static PropertyChangedMessage With(string name) => new(name);
    }
}