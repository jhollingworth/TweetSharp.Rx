using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Demo.WindowsMobile.Extensions
{
    internal static class CollectionExtensions
    {
        internal static void ForEach(this Control.ControlCollection collection, Action<Control> action)
        {
            foreach (Control c in collection)
            {
                action(c); 
            }
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}
