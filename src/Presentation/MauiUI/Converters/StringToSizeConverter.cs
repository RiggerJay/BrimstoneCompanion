using System.Globalization;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Converters
{
    public class StringToSizeConverter : IValueConverter
    {
        public Enum Enum { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return e.ToString();
            }
            return 26;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}