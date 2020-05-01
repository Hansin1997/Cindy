namespace Cindy.Strings
{
    interface IStringSource
    {
        string Get(string key, string defaultValue = default);
    }
}
