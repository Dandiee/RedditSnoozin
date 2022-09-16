using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlizzCheck
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public static readonly SnakeCaseNamingPolicy Default = new();
        public override string ConvertName(string name)
        {
            static IEnumerable<char> ToSnakeCase(CharEnumerator e)
            {
                if (!e.MoveNext()) yield break;
                yield return char.ToLower(e.Current, CultureInfo.InvariantCulture);
                while (e.MoveNext())
                {
                    if (char.IsUpper(e.Current))
                    {
                        yield return '_';
                        yield return char.ToLower(e.Current, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        yield return e.Current;
                    }
                }
            }

            return new string(ToSnakeCase(name.GetEnumerator()).ToArray());
        }
    }

}
