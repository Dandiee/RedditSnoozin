using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlizzCheck.Models
{
    [JsonConverter(typeof(ReplyConverter))]
    public class CommentBase
    {
        public CommentBaseData data { get; set; }
    }

    public class CommentBaseData
    {
        public List<CommentChild> children { get; set; }
    }

    public class CommentChild
    {
        public CommentChildData data { get; set; }
    }

    public class CommentChildData
    {
        public string permalink { get; set; }
        public float created_utc { get; set; }
        public string author { get; set; }
        public string id { get; set; }
        public string body { get; set; }

        [JsonConverter(typeof(ReplyConverter))]
        public CommentBase replies { get; set; }
    }

    public class ReplyConverter : JsonConverter<CommentBase>
    {
        public override CommentBase? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return null;
            }

            while (!(reader.TokenType == JsonTokenType.PropertyName && reader.GetString() == "data"))
            {
                reader.Read();
            }

            
            var data = JsonSerializer.Deserialize<CommentBaseData>(ref reader, options);

            reader.Read();
            return new CommentBase
            {
                data = data
            };
        }

        public override void Write(Utf8JsonWriter writer, CommentBase value, JsonSerializerOptions options)
            => JsonSerializer.Serialize(writer, value, options);
    }
}
