namespace WaCore.Contracts.Data.Filters.Base
{
    public class FilterBase
    {
        public int Offset { get; set; }

        public int? Limit { get; set; }

        public string SortField { get; set; }

        public bool SortOrderIsAscending { get; set; }

        public FilterBase()
        {
            Offset = 0;
            Limit = 50;
            SortOrderIsAscending = true;
        }
    }
}
