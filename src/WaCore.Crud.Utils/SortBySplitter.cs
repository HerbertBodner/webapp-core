using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace WaCore.Crud.Utils
{
    public static class SortBySplitter
    {
        public static Regex SortRegex = new Regex("[+-]\\w+", RegexOptions.Compiled);

        public static List<OrderItem> SplitSortByString(string sortByString)
        {
            var list = new List<OrderItem>();
            if (string.IsNullOrEmpty(sortByString))
            {
                return list;
            }

            sortByString = AddLeadingPlusIfNecessary(sortByString);

            foreach (var match in SortRegex.Matches(sortByString))
            {
                var order = match.ToString().StartsWith('+') ? OrderItem.OrderBy.Ascending : OrderItem.OrderBy.Descending;

                var fieldName = match.ToString().Substring(1);

                list.Add(new OrderItem
                {
                    OrderDirection = order,
                    FieldName = fieldName
                });
            }

            return list;
        }

        private static string AddLeadingPlusIfNecessary(string sortByString)
        {
            if (!sortByString.StartsWith('+') && !sortByString.StartsWith('-'))
            {
                sortByString = "+" + sortByString;
            }

            return sortByString;
        }
    }

    

    public class OrderItem
    {
        public OrderBy OrderDirection { get; set; }
        public string FieldName { get; set; }

        public enum OrderBy
        {
            Ascending = 1,
            Descending = 2
        }
    }
}
