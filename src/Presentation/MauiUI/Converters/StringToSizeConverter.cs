using System.Globalization;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Converters
{
    public class StringToSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value?.ToString() ?? string.Empty;
            _ = int.TryParse(parameter?.ToString() ?? string.Empty, out int len);

            len += str.Length;

            if (len < 5)
            {
                return 28;
            }
            else if (len < 6)
            {
                return 24;
            }
            else
            {
                return 20;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}