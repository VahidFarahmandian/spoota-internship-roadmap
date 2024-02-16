using StackExchange.Redis;
using System.Text.Json;

namespace FirstWeb.API.Services
{
    public class CacheServiceDistributed : ICacheServiceDistributed
    {
        private IDatabase _cacheDb;
        public CacheServiceDistributed()
        {
            var redis = ConnectionMultiplexer.Connect("127.0.0.1:6379,abortConnect=false");
            _cacheDb = redis.GetDatabase();
        }
        public T getData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);
            if (!string.IsNullOrEmpty(value))
                return JsonSerializer.Deserialize<T>(value);

            return default;
        }
        public object removeData(string key)
        {
            var isExist = _cacheDb.KeyExists(key);

            if (isExist)
                return _cacheDb.KeyDelete(key);

            return default;
        }

        public bool setData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
            return isSet;
        }


    }
}
