using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static PostmanCloneLibrary.Enums;

namespace PostmanCloneLibrary
{
    public class ApiAccess
    {

        private readonly HttpClient client = new HttpClient();

        public async Task<string> CallApiAsync(
            string url, 
            bool formatOutput = true, 
            HttpAction action = HttpAction.GET
        )
        {
            var resonse = await client.GetAsync(url);

            if (resonse.IsSuccessStatusCode)
            {
                string json = await resonse.Content.ReadAsStringAsync();

                if (formatOutput)
                {
                    var jsonElement = JsonSerializer.Deserialize<JsonElement>(json);
                    json = JsonSerializer.Serialize(jsonElement,
                        new JsonSerializerOptions { WriteIndented = true });
                }

                return json;
            }
            else
            {
                return $"Error: {resonse.StatusCode}";
            }
        }

        public bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            bool output = Uri.TryCreate(url, UriKind.Absolute, out Uri uriOutput) && (uriOutput.Scheme == Uri.UriSchemeHttps);

            return output;
        }
    }
}
