namespace vetappback.DTOs
{
    public class PaginationsDTO
    {
         public int Page { get; set; } = 1;

        private int recordsbyPage = 10;
        private readonly int maxPagecount = 50;

        public int RecordsbyPage
        {
            get
            {
                return recordsbyPage;
            }
            set
            {
                recordsbyPage = (value > maxPagecount) ? maxPagecount : value;
            }
        }
    }
}