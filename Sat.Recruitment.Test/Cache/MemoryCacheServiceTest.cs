using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Moq;
using Sat.Recruitment.Api.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Cache
{

    [CollectionDefinition("FactoryTest", DisableParallelization = true)]
    public class MemoryCacheServiceTest
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IOptions<CacheConfiguration> _cacheConfig;
        private readonly MemoryCacheService _memoryCacheService;

        public MemoryCacheServiceTest()
        {
            _cacheConfig = Options.Create(new CacheConfiguration()
            {
                AbsoluteExpirationInHours = 1,
                SlidingExpirationInMinutes = 60
            });
            _memoryCache = new MemoryCache(new MemoryCacheOptions
            {



            });
            _memoryCacheService = new MemoryCacheService(_memoryCache, _cacheConfig);
        }

        [Fact]
        public void TryGet_returns_value()
        {
            string cacheKey = "testKey";
            int value = 123;

            _memoryCacheService.Set(cacheKey, value);
            bool result = _memoryCacheService.TryGet(cacheKey, out value);

            Assert.True(result);

        }
        [Fact]
        public void TryGet_returns_false()
        {
            string cacheKey = "testKey";
            int? value = null;


            bool result = _memoryCacheService.TryGet(cacheKey, out value);

            Assert.False(result);
        }

        [Fact]
        public void Set()
        {
            string cacheKey = "testKey";
            int value = 123;
            _memoryCacheService.Set(cacheKey, value);
            var flag = _memoryCacheService.TryGet(cacheKey, out int result);

            Assert.True(flag);
            Assert.Equal(value, result);

        }

        [Fact]
        public void Remove()
        {
            string cacheKey = "testKey";
            int value = 123;
            _memoryCacheService.Set(cacheKey, value);

            var addedFlag = _memoryCacheService.TryGet(cacheKey, out int addedValue);

            Assert.True(addedFlag);

            _memoryCacheService.Remove(cacheKey);
            var removedFlag = !_memoryCacheService.TryGet(cacheKey, out int result);

            Assert.True(removedFlag);


        }
    }
}
