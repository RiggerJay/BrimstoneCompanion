using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;
using System.Globalization;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Converters
{
    public class CalculatedAttributeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
            {
                return 0;
            }
            ObservableAttribute? attribute = null;
            ObservableCharacter? character = null;

            foreach (var value in values)
            {
                if (value is ObservableCharacter)
                {
                    character = value as ObservableCharacter;
                }
                else if (value is ObservableAttribute)
                {
                    attribute = value as ObservableAttribute;
                }
            }

            if (character == null
                || attribute == null)
            {
                return 0;
            }
            return character.GetCalculatedValue(attribute);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}