using Newtonsoft.Json;

namespace JibbleInterviewTest.Models
{
    public class PeopleModel
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Gender { get; set; }
        public string? Age { get; set; }
        [JsonProperty("Emails")]
        public List<string> Emails { get; set; }
        public string? FavoriteFeature { get; set; }
        public List<string> Features { get; set; }
        public List<AddressModel> AddressInfo { get; set; } 
    }
}
