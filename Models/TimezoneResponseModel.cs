namespace BTVN.Models
{
    public class TimezoneResponseModel
    {
        public Guid id { get;set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int offset { get; set; }
        public bool? isDTS { get; set; }
        public string[] utc { get; set; }
        public int? order { get; set; }
        public Guid createdByUserId { get; set; }
        public Guid lastModifiedByUserId { get; set; }
        public DateTime lastModifiedOnDate { get; set; }
        public DateTime lastModifiedOnTime { get; set;}

        public DateTime lastModifiedOnTimeUtc { get; set; }
    }
}
