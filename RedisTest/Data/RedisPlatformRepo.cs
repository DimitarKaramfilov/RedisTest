using RedisTest.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisTest.Data
{
    public class RedisPlatformRepo : IPlatformRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPlatformRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }


        public void CreatePlatform(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
                       
            string serializedPlatform = JsonSerializer.Serialize(platform);
            IDatabase db = _redis.GetDatabase();
            //db.StringSet(platform.Id, serializedPlatform);
            db.SetAdd("Platforms", serializedPlatform);
        }

        public IEnumerable<Platform?>? GetAllPlatforms()
        {
            IDatabase db = _redis.GetDatabase();
            RedisValue[] completeSet = db.SetMembers("Platforms");

            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<Platform>(val)).ToList();
                return obj;
            }

            return null;
        }

        public Platform? GetPlatform(string id)
        {
            IDatabase db = _redis.GetDatabase();            
            var platfromString = db.StringGet(id);

            if (!string.IsNullOrEmpty(platfromString))
            {
                return JsonSerializer.Deserialize<Platform>(platfromString);
            }
            return null;
        }
    }
}
