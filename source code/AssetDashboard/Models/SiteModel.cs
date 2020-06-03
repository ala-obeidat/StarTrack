namespace StarTrack.Dashboard.Models
{
    public class SiteModel
    {
        public string SiteName { get; set; }
        public string SiteTagId { get; set; }
        public string SiteDetail { get; set; }
        public string SiteStreet { get; set; }
        public string SiteCity { get; set; }
        public string SiteState { get; set; }
        public string SitePostalCode { get; set; }
        public string SiteCountry { get; set; }
        public SiteStatus SiteStatus { get; set; }
    }
    public enum SiteStatus
    {
        Normal =1
    }
}