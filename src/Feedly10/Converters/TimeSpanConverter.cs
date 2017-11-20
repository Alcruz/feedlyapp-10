using System;

namespace Feedly10.App.Converters
{
    class TimeSpanConverter
    {
        public static DateTime ConvertToDate(long timeSpan)
        {
            DateTime currentDate = DateTime.Now ;
            currentDate = DateTime.Now;
            TimeSpan ts = new TimeSpan(timeSpan);
            DateTime dateConverted = currentDate - ts;
            return dateConverted;
        }
    }
}
