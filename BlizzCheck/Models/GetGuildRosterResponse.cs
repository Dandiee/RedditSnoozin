namespace BlizzCheck.Models
{
    public class GetGuildRosterResponse
    {
        public Member[] Members { get; set; }
    }

    public class Member
    {
        public Character Character { get; set; }
        public int Rank { get; set; }
    }

    public class Character
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Level { get; set; }
        public IdData PlayableClass { get; set; }
        public IdData PlayableRace { get; set; }
    }

    public class IdData
    {
        public Key Key { get; set; }
        public int Id { get; set; }
    }
}
