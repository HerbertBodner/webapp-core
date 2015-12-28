using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Web.ViewModels
{
    public interface ISearchConfig
    {
        int Limit { get; set; }

        int Offset { get; }

        string SortField { get; set; }

        bool SortOrderIsAscending { get; set; }

        int Page { get; set; }

        string OrderBy { get; set; }
    }


    public abstract class SearchConfig<TFilter> : ISearchConfig
        where TFilter : class
    {
        public int Limit { get; set; }

        public int Offset => (Page - 1) * Limit;

        public string SortField { get; set; }

        public bool SortOrderIsAscending { get; set; }

        public int Page { get; set; }

        /// <summary>
        /// OrderBy property contains information about SortField and SortOrderIsAscending properties.
        /// It is used in the view to store information about both properties in one single location, 
        /// so it can be used to submit the information on one button click and we don´t need a custom model-binder.
        /// </summary>
        public string OrderBy
        {
            get { return $"{SortField} {(SortOrderIsAscending ? "asc" : "desc")}"; }
            set
            {
                var settings = value.Split(' ');
                if (settings.Length == 2 &&
                    !string.IsNullOrEmpty(settings[0]) &&
                    !string.IsNullOrEmpty(settings[1]))
                {
                    SortField = settings[0];
                    SortOrderIsAscending = settings[1].Equals("asc", StringComparison.OrdinalIgnoreCase);
                }
            }
        }

        public SearchConfig()
        {
            Limit = 50;
            SortOrderIsAscending = true;
            Page = 1;
        }


        public abstract TFilter ToFilter();
    }
}
