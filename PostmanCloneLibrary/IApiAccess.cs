
namespace PostmanCloneLibrary
{
    public interface IApiAccess
    {
        Task<string> CallApiAsync(string url, bool formatOutput = true, Enums.HttpAction action = Enums.HttpAction.GET);
        bool IsValidUrl(string url);
    }
}