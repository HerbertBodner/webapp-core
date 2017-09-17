using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace WaCore.Crud.Utils
{
    public static class SortBySplitter
    {
        public static Regex SortRegex = new Regex("[+-]?[\\w\\.]+", RegexOptions.Compiled);

        public static List<OrderItem> SplitSortByString(string sortByString)
        {
            var list = new List<OrderItem>();
            if (string.IsNullOrEmpty(sortByString))
            {
                return list;
            }

            foreach (var match in SortRegex.Matches(sortByString))
            {
                var order = match.ToString().StartsWith('-') ? OrderItem.OrderBy.Descending : OrderItem.OrderBy.Ascending;

                var fieldName = match.ToString().TrimStart('+','-');

                list.Add(new OrderItem
                {
                    OrderDirection = order,
                    FieldName = fieldName
                });
            }

            return list;
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
