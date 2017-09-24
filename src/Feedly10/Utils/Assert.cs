using System;

namespace Feedly10.App.Utils
{
    public class Assert
    {
        public static void IsNotNull<T>(T param, string paramName)
        {
            if (param == null)
                throw new ArgumentNullException(paramName);
        }
    }
}
