using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common.Extensions
{
    public static class ListExtensions
    {
        public static void AddAtPosition<T>(this List<T> myList, T value, int order)
        {
            if (order < myList.Count)
            {
                myList.Add(value);
            }
            else
            {
                myList.Insert(order, value);
            }
        }
    }
}
