namespace BTVN.Models
{
    public class TimezoneResponseModelPagination
    {
        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; }
        public int numberOfRecords { get; set; }
        public int totalRecords { get; set; }
        public IEnumerable<TimezoneResponseModel> content {get;set;}
    }
}
