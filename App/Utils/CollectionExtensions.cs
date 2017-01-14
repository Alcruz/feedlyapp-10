using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Utils
{
    public static class CollectionExtensions
    {
        public static void Add<T1, T2>(this ICollection<KeyValuePair<T1, T2>> target, T1 key, T2 value)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            target.Add(new KeyValuePair<T1, T2>(key, value));
        }
    }
}
