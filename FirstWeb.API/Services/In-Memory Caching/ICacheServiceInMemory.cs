namespace FirstWeb.API.Services.In_Memory_Caching
{
    public interface ICacheServiceInMemory
    {
        T getData<T>(string key);
        bool setData<T>(string key, T data, DateTimeOffset expirationTime);
        object removeData(string key);
    }
}
