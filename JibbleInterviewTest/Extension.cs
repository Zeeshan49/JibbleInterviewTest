using JibbleInterviewTest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace JibbleInterviewTest
{
    public static class Extension
    {
        public static T Deserialize<T>(this string SerializedJSONString)
        {
            if (!IsValidJson(SerializedJSONString))
            {
                throw new ArgumentException("Invalid Json");
            }

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
            StringBuilder output = new StringBuilder();
            output = output.AppendLine("\r\n\r\n" +
               $"First Name: {model.FirstName}" +
               $" Middle Name: {model.MiddleName} " +
               $" Last Name: {model.LastName} " +
               $" User Name: {model.UserName}" +
               $"\r\n\r\n" +
               $"Gender:{model.Gender} " +
               $"Age:{model.Age}" +
               $"\r\n\r\n" +
               $"Favorite Feature:{model.FavoriteFeature}");            
            
            output.AppendLine($"Emails :");
            foreach (var item in model.Emails)
                output.AppendLine(item);

            output.AppendLine($"Features :");
            foreach (var item in model.Features)
                output.AppendLine(item);

            output.AppendLine($"Address Info:");

            foreach (var item in model.AddressInfo)
            {
                output.AppendLine($"Address : {item.Address}");
                output.AppendLine($"City :");               
                output.AppendLine($" Name: {item.City.Name}" +
                        $" CountryRegion: {item.City.CountryRegion} " +
                        $" Region: {item.City.Region} ");
            }
            Console.WriteLine($"{output.ToString()}");
        }

        public static void Display(this PeopleRowModel model)
        {
            foreach (var item in model.Value)
            {
                Console.WriteLine($"\r\n\r\n User Name: {item.UserName} First Name: {item.FirstName} Last Name: {item.LastName}");
            }
        }
    }
}
