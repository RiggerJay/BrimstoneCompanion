using System.Globalization;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Converters
{
    public class StringToEnumConverter : IValueConverter
    {
        public Enum Enum { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum e)
            {
                return e.ToString();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse(targetType, (string)value, true);
        }
    }
}