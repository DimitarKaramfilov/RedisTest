using RedisTest.Models;

namespace RedisTest.Data
{
    public interface IPlatformRepo
    {
        void CreatePlatform(Platform platform);
        
        Platform GetPlatform(string id);

        IEnumerable<Platform?>? GetAllPlatforms();
    }
}
