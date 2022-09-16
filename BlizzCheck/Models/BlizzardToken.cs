namespace BlizzCheck.Models
{
    public class BlizzardToken
    {
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string Scope { get; set; }
    }
}
