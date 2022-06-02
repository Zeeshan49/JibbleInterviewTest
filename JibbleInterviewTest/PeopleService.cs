using Newtonsoft.Json;

namespace JibbleInterviewTest
{
    public interface IPeopleService
    {
        Task<PeopleRowModel> Get(SearchRequest request);
        Task<PeopleRowModel> Get();
        Task<PeopleModel> GetById(string Id);
    }

    public class PeopleService : IPeopleService
    {
        private readonly HttpClient _httpClient;

        public PeopleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PeopleRowModel> Get(SearchRequest request)
        {
            string? apiurl = $"People?$filter=FirstName eq '{request.Value}'";
            HttpResponseMessage? response = await _httpClient.GetAsync(apiurl);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleRowModel>();

        }

        public async Task<PeopleRowModel> Get()
        {
            string? apiurl = $"people";
            HttpResponseMessage? response = await _httpClient.GetAsync(apiurl);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleRowModel>();
        }

        public async Task<PeopleModel> GetById(string id)
        {
            string? apiurl = $"People('{id}')";
            HttpResponseMessage? response = await _httpClient.GetAsync(apiurl);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleModel>();
        }

    }

    public class PeopleModel
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class PeopleRowModel
    {
        //[JsonProperty("odata.metadata")]
        //public string odatametadata { get; set; }
        [JsonProperty("value")]
        public List<PeopleModel> Value { get; set; }
    }

    public class ModelPageLocationList
    {
        [JsonProperty("odata.metadata")]
        public string odatametadata { get; set; }
        [JsonProperty("value")]
        public List<PeopleModel> value { get; set; }
    }

    public class SearchRequest
    {
        public string? Col { get; set; }
        public string? Value { get; set; }
    }

    public class SearchModel
    {
        public string? Value { get; set; }
    }
}



//Excelption issue resolved 
//https://stackoverflow.com/questions/44219455/cannot-deserialize-the-current-json-array-with-metadata-and-records