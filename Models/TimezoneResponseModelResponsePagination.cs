namespace BTVN.Models
{
    public class TimezoneResponseModelResponsePagination
    {
        public int code { get;set; }
        public string message { get;set; } 
        public string licenseInfo { get; set; }
        public int totalTime { get;set; }
        public TimezoneResponseModelPagination data { get; set; }
    }
}
