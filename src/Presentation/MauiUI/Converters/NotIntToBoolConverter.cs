using System.Collections;
using System.Globalization;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Converters
{
    public class NotIntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            _ = int.TryParse(parameter?.ToString() ?? string.Empty, out int count);

            if (value is IList list)
            {
                return list.Count != count;
            }

            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}