﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static PostmanCloneLibrary.Enums;

namespace PostmanCloneLibrary
{
    public class ApiAccess : IApiAccess
    {

        private readonly HttpClient client = new HttpClient();

        public async Task<string> CallApiAsync(
            string url,
            string content,
            HttpAction action = HttpAction.GET,
            bool formatOutput = true)
        {
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            return await CallApiAsync(url, stringContent, action, formatOutput);
        }

        public async Task<string> CallApiAsync(
            string url,
            HttpContent? content = null,
            HttpAction action = HttpAction.GET,
            bool formatOutput = true
        )
        {
            HttpResponseMessage? response;

            switch (action)
            {
                case HttpAction.GET:
                    response = await client.GetAsync(url);
                    break;
                case HttpAction.POST:
                    response = await client.PostAsync(url, content);
                    break;
                case HttpAction.PUT:
                    response = await client.PutAsync(url, content);
                    break;
                case HttpAction.PATCH:
                    response = await client.PatchAsync(url, content);
                    break;
                case HttpAction.DELETE:
                    response = await client.DeleteAsync(url);
                    break;
                default:
                    throw new ArgumentException($"Unsupported HTTP action: {action}", nameof(action));
            }

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

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
                return $"Error: {response.StatusCode}";
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
