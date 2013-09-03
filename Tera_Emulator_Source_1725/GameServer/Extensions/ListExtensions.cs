using System;
using System.Collections.Generic;

namespace Tera.Extensions
{
    public static class ListExtensions
    {
        public static void Map<T>(this List<T> list, Action<T> action)
        {
            if (list == null)
                return;

            for (int i = 0; i < list.Count; i++)
                action(list[i]);
        }

        public static void Each<T>(this List<T> list, Action<T> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T item;

                try
                {
                    item = list[i];
                }
                catch
                {
                    continue;
                }

                action(item);
            }
        }

        public static List<T> Select<T>(this List<T> list, Predicate<T> action)
        {
            List<T> selected = new List<T>();

            for (int i = 0; i < list.Count; i++)
            {
                T item;

                try
                {
                    item = list[i];
                }
                catch
                {
                    continue;
                }

                if (action(item))
                    selected.Add(item);
            }

            return selected;
        }
    }
}
