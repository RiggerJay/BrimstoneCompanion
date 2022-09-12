using MediatR;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Notifications
{
    public class AttributeValueChangedNotification : INotification
    {
        public string AttributeName { get; }

        private AttributeValueChangedNotification(string attributeName)
        {
            AttributeName = attributeName;
        }

        public static AttributeValueChangedNotification WithName(string attributeName) => new(attributeName);
    }
}