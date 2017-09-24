using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Feedly10.App.Converters
{
    public class HtmlStripperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var html = value as string;
            return html;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}