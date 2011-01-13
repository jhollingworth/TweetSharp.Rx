#region License

// TweetSharp
// Copyright (c) 2010 Daniel Crenna and Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Hammock.Caching;

namespace TweetSharp.Twitter.Extras.Core.Caching
{
    public class MemcachedCache : ICache
    {
        private readonly MemcachedClient _client = new MemcachedClient();

        public MemcachedCache()
        {
            _client = new MemcachedClient();
        }

        public MemcachedCache(string sectionName)
        {
            _client = new MemcachedClient(sectionName);
        }

        public MemcachedCache(IMemcachedClientConfiguration configuration)
        {
            _client = new MemcachedClient(configuration);
        }

        #region IClientCache Members

        public void Insert(string key, object value)
        {
            _client.Store(StoreMode.Add, key, value);
        }

        public void Insert(string key, object value, DateTime absoluteExpiration)
        {
            _client.Store(StoreMode.Add, key, value, absoluteExpiration);
        }

        public void Insert(string key, object value, TimeSpan slidingExpiration)
        {
            _client.Store(StoreMode.Add, key, value, slidingExpiration);
        }

        public T Get<T>(string key)
        {
            return _client.Get<T>(key);
        }

        public void Remove(string key)
        {
            _client.Remove(key);
        }

        #endregion
    }
}