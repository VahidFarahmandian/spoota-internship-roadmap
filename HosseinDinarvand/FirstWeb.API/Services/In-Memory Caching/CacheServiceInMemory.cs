
using System.Runtime.Caching;

namespace FirstWeb.API.Services.In_Memory_Caching
{
    public class CacheServiceInMemory : ICacheServiceInMemory
    {
        private ObjectCache _memoryCache = MemoryCache.Default;
        public T getData<T>(string key)
        {
            try
            {
                T item = (T)_memoryCache.Get(key);
                return item;
            }

            catch (Exception ex)
            {

                throw;
            }
        }

        public object removeData(string key)
        {
            var result = true;

            try
            {
                if (!string.IsNullOrEmpty(key))
                    _memoryCache.Remove(key);
                else
                    result = false;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool setData<T>(string key, T data, DateTimeOffset expirationTime)
        {
            bool result = true;
            try
            {
                if (!string.IsNullOrEmpty(key))
                    _memoryCache.Set(key, data, expirationTime);
                else
                    result = false;

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
