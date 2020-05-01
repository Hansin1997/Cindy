namespace Cindy.UI.Binder
{
    public interface IDataSource
    {
        T GetData<T>(string key, T defaultValue = default);

        void SetData(string key, object value);

        bool IsReadable();

        bool IsWriteable();
    }
}
