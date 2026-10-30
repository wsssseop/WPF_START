using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Core.ValueConverters
{
    [ValueConversion(typeof(string), typeof(SolidColorBrush))]
    public class StringRGBToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string rgb)
                return Brushes.Transparent;

            string hex = rgb.StartsWith('#') ? rgb : $"#{rgb}";

            try
            {

                var color = (Color)ColorConverter.ConvertFromString(hex);
                var brush = new SolidColorBrush(color);
                brush.Freeze();
                return brush;
            }
            catch(Exception ex)
            {
                return Brushes.Transparent;   // "zzzzzz"처럼 잘못된 값
            }



        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
