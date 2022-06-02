using Newtonsoft.Json;
using System.Diagnostics;

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
        private HttpClient _httpClient;

        public PeopleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PeopleRowModel> Get(SearchRequest request)
        {
            var apiurl = $"People?$filter=FirstName eq '{request.Value}'";
            var response = await _httpClient.GetAsync(apiurl);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"{response.StatusCode}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleRowModel>();

        }

        public async Task<PeopleRowModel> Get()
        {
            var apiurl = $"people";
            var response = await _httpClient.GetAsync(apiurl);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"{response.StatusCode}");

            var jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleRowModel>();
        }

        public async Task<PeopleModel> GetById(string id)
        {
            var apiurl = $"People('{id}')";
            var response = await _httpClient.GetAsync(apiurl);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"{response.StatusCode}");

            var str = await response.Content.ReadAsStringAsync();

            if (str.IsValidJson())
                return str.Deserialize<PeopleModel>();

            else
                return null;

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