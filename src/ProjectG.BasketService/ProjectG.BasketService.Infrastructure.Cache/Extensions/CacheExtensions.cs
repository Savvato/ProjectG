namespace ProjectG.BasketService.Infrastructure.Cache.Extensions
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Distributed;

    using Newtonsoft.Json;

    public static class CacheExtensions
    {
        public static async Task<T> Get<T>(this IDistributedCache cache, string key) where T : class
        {
            string result = await cache.GetStringAsync(key);
            return result == null ? null : JsonConvert.DeserializeObject<T>(result);
        }

        public static async Task Set<T>(this IDistributedCache cache, string key, T value) where T : class
        {
            string result = JsonConvert.SerializeObject(value);

            await cache.SetStringAsync(key, result);
        }

        public static async Task Set<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions cacheEntryOptions) where T : class
        {
            string result = JsonConvert.SerializeObject(value);

            await cache.SetStringAsync(key, result, cacheEntryOptions);
        }
    }
}
