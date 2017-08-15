namespace WaCore.Contracts.Data.Filters.Base
{
    public class WacFilterBase
    {
        public int Offset { get; set; }

        public int? Limit { get; set; }

        public string SortField { get; set; }

        public bool SortOrderIsAscending { get; set; }

        public WacFilterBase()
        {
            Offset = 0;
            Limit = 50;
            SortOrderIsAscending = true;
        }
    }
}
