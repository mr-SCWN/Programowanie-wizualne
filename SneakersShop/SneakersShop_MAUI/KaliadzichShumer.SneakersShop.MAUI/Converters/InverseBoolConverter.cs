using System.Globalization;

namespace KaliadzichShumer.SneakersShop.MAUI.Converters{
    public class InverseBoolConverter : IValueConverter {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
            if (value is bool boolValue) {
                return !boolValue;
            } else {
                return value;
            }
          
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue) {
                return !boolValue;
            } else {
                 return value;
            }
           
        }
    }
}
