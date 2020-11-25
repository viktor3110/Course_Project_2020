﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiskRental.Services
{
    public class CachingService
    {
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private readonly IMemoryCache _cache;
        public CachingService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void Set<T>(string key, T obj)
        {
            var options = new MemoryCacheEntryOptions();
            options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));
            _cache.Set(key, obj, options);
        }
        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }
        public bool TryGetValue<T>(string key, out T obj)
        {
            if (_cache.TryGetValue(key, out obj))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Clean()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();
        }
    }
}
