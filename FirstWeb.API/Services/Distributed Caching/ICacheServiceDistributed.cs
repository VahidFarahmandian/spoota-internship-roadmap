﻿namespace FirstWeb.API.Services
{
    public interface ICacheServiceDistributed
    {
        T getData<T>(string key);
        bool setData<T>(string key, T data,DateTimeOffset expirationTime);
        object removeData(string key);
    }
}
