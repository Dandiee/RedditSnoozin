using System;

namespace BlizzCheck.Models
{
    public class SubmitRoot
    {
        public RawSubmit[] data { get; set; }
    }

    public class RawSubmit
    {
        public string author { get; set; }
        public string author_fullname { get; set; }
        public long created_utc { get; set; }
        public string full_link { get; set; }
        public string id { get; set; }
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
