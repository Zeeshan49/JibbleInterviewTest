using Newtonsoft.Json;

namespace JibbleInterviewTest.Models
{
    public class PeopleRowModel
    {
        //[JsonProperty("odata.metadata")]
        //public string odatametadata { get; set; }
        [JsonProperty("value")]
        public List<PeopleModel> Value { get; set; }
    }
}
