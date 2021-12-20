public interface IDataStore
{
    void SetData<T>(T data, string fileName);
    T GetData<T>(string fileName);
}
