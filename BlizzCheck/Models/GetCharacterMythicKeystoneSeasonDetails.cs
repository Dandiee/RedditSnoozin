using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlizzCheck.Models.Mdi
{

    public class GetCharacterMythicKeystoneSeasonDetails
    {
        [JsonPropertyName("_links")]
        public Links Links { get; set; }
        public Season Season { get; set; }
        public BestRuns[] BestRuns { get; set; }
        public Character Character { get; set; }
        public MythicRating MythicRating { get; set; }
    }

    public class Links
    {
        public Self Self { get; set; }
    }

    public class Self
    {
        public string Href { get; set; }
    }

    public class Season
    {
        public Key Key { get; set; }
        public int Id { get; set; }
    }

 
    public class Character
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public RealmWithName RealmWithName { get; set; }
    }

  
    public class RealmWithName
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Slug { get; set; }
    }

 
    public class MythicRating
    {
        public float Rating { get; set; }
    }

    public class BestRuns
    {
        public long CompletedTimestamp { get; set; }
        public int Duration { get; set; }
        public int KeystoneLevel { get; set; }
        public KeystoneAffixes[] KeystoneAffixes { get; set; }
        public Member[] Members { get; set; }
        public Dungeon Dungeon { get; set; }
        public bool IsCompletedWithinTime { get; set; }
        public MythicRating1 MythicRating { get; set; }
        public MapRating MapRating { get; set; }
    }

    public class Dungeon
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
    

    public class MythicRating1
    {
        public float Rating { get; set; }
    }

 

    public class MapRating
    {
        public float Rating { get; set; }
    }


    public class KeystoneAffixes
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }


    public class Member
    {
        public Character1 Character { get; set; }
        public Specialization Specialization { get; set; }
        public Race Race { get; set; }
        public int EquippedItemLevel { get; set; }
    }

    public class Character1
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Realm Realm { get; set; }
    }

    public class Realm
    {
        public Key Key { get; set; }
        public int Id { get; set; }
        public string Slug { get; set; }
    }


    public class Specialization
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }


    public class Race
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    

}
