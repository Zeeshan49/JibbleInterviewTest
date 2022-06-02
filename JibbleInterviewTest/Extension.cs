using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JibbleInterviewTest
{
    public static class Extension
    {
        public static T Deserialize<T>(this string SerializedJSONString)
        {
            if (!IsValidJson(SerializedJSONString))
                throw new ArgumentException("Invalid Json");

            return JsonConvert.DeserializeObject<T>(SerializedJSONString);
        }

        public static bool IsValidJson(this string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    JToken? obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void Display(this PeopleModel model)
        {
            Console.WriteLine($" First Name: {model.FirstName} " +
               $" Last Name: {model.LastName} " +
               $" User Name: {model.UserName} ");
        }

        public static void Display(this PeopleRowModel model)
        {
            foreach (var item in model.Value)
                Console.WriteLine($"User Name: {item.UserName} First Name: {item.FirstName} Last Name: {item.LastName}");
        }
    }
}
