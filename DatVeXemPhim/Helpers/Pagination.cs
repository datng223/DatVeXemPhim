namespace DatVeXemPhim.Helpers
{
    public class Pagination
    {
        public int PageSize {  get; set; }
        public int PageNumber {  get; set; }
        public int TotalCount {  get; set; }
        public int TotalPage
        {
            get
            {
                if (PageSize == 0) return 0;
                var total = TotalCount / PageSize;
                if (TotalCount % PageSize > 0) total++;
                return total;
            }
        }

        public Pagination()
        {
            PageNumber = 1;
            PageSize = -1;
        }
    }
}
