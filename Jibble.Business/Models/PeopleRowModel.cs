using Newtonsoft.Json;

namespace Jibble.Business.Models
{
    public class PeopleRowModel
    {       
        [JsonProperty("value")]
        public List<PeopleModel> Data { get; set; }
    }
}
