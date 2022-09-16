using System.IO;
using System.Text.Json;

namespace BlizzCheck
{
    public sealed class CacheService
    {
        public const string CachePath = "C:\\projects\\cache2";

        public string GetData(string key)
        {
            var path = $"{CachePath}\\{key}.dat";
            var fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                return File.ReadAllText(path);
            }

            return null;
        }

        public void SetData(string key, string value)
        {
            var path = $"{CachePath}\\{key}.dat";
            var fileInfo = new FileInfo(path);
            var dir = fileInfo.Directory;
            if (!dir.Exists)
            {
                dir.Create();
            }
            File.WriteAllText(path, value);
        }

        public void SetData<T>(string key, T data)
        {
            var json = JsonSerializer.Serialize(data);
            this.SetData(key, json);
        }

    }
}
