using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utils
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
