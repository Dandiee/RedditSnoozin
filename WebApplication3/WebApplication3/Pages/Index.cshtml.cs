using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication3.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<UserRoot> Roots = new List<UserRoot>()
                ;
        private const string DataRoot = @"c:\Projects\cache2";
        public void OnGet()
        {
            var path = $"{DataRoot}\\authors.dat";
            var datttta = TryReadCache<UserRoot>(path);
            var f = new DateTimeOffset(2022, 06, 10, 0, 0, 0, TimeSpan.Zero);
            var t = new DateTimeOffset(2022, 07, 08, 23, 59, 59, TimeSpan.Zero);
            Roots = datttta
                .Where(s => s.data.CreatedDt > f && s.data.CreatedDt < t)
                .Where(s => !string.IsNullOrEmpty(s.data.snoovatar_img))
                .OrderBy(s => s.data.CreatedDt).ToList();
        }

        private List<T> TryReadCache<T>(string filePath)
        {
            var fi = new FileInfo(filePath);
            if (fi.Exists)
            {
                var text = System.IO.File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<List<T>>(text);

                return data;
            }

            return new List<T>();
        }

    }

    public class UserRoot
    {
        public string kind { get; set; }
        public UserData data { get; set; }
    }

    public class UserData
    {
        public string name { get; set; }
        public float created_utc { get; set; }
        public DateTimeOffset CreatedDt => DateTimeOffset.FromUnixTimeSeconds((long)created_utc);
        public string snoovatar_img { get; set; }
    }
}