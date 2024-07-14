

namespace PostmanCloneLibrary
{
    public interface IApiAccess
    {
        Task<string> CallApiAsync(string url, string content, Enums.HttpAction action = Enums.HttpAction.GET, bool formatOutput = true);
        Task<string> CallApiAsync(string url, HttpContent? content = null, Enums.HttpAction action = Enums.HttpAction.GET, bool formatOutput = true);
        bool IsValidUrl(string url);
    }
}