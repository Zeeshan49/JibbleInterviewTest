using Newtonsoft.Json;

namespace JibbleInterviewTest.Models
{
    public class ModelPageLocationList
    {
        [JsonProperty("odata.metadata")]
        public string odatametadata { get; set; }
        [JsonProperty("value")]
        public List<PeopleModel> value { get; set; }
    }
}
