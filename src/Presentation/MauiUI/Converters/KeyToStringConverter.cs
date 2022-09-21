using RedSpartan.BrimstoneCompanion.AppLayer.Interfaces;
using System.Globalization;

namespace RedSpartan.BrimstoneCompanion.MauiUI.Converters
{
    public class KeyToStringConverter : BindableObject, IValueConverter
    {
        public static readonly BindableProperty TextResourceProperty =
            BindableProperty.Create(nameof(TextResource), typeof(ITextResource), typeof(KeyToStringConverter));

        public ITextResource TextResource
        {
            get => (ITextResource)GetValue(TextResourceProperty);
            set => SetValue(TextResourceProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (TextResource != null)
            {
                return TextResource.GetValue((string)value);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}