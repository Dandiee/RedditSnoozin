using System;
using BlizzCheck.Extensions;
using System.Text.Json.Serialization;

namespace BlizzCheck.Models.CharProfile
{

    public class GetCharacterProfileResponse
    {
        [JsonPropertyName("_links")]
        public Links Links { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public Faction Faction { get; set; }
        public Race Race { get; set; }
        public CharacterClass CharacterClass { get; set; }
        public ActiveSpec ActiveSpec { get; set; }
        public Realm Realm { get; set; }
        public Guild Guild { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int AchievementPoints { get; set; }
        public Achievements Achievements { get; set; }
        public Titles Titles { get; set; }
        public PvpSummary PvpSummary { get; set; }
        public Encounters Encounters { get; set; }
        public Media Media { get; set; }
        public long LastLoginTimestamp { get; set; }
        public int AverageItemLevel { get; set; }
        public int EquippedItemLevel { get; set; }
        public Specializations Specializations { get; set; }
        public Statistics Statistics { get; set; }
        public MythicKeystoneProfile MythicKeystoneProfile { get; set; }
        public Equipment Equipment { get; set; }
        public Appearance Appearance { get; set; }
        public Collections Collections { get; set; }
        public Reputations Reputations { get; set; }
        public Quests Quests { get; set; }
        public AchievementsStatistics AchievementsStatistics { get; set; }
        public Professions Professions { get; set; }
        public CovenantProgress CovenantProgress { get; set; }
    }

    public class Link
    {
        public Self Self { get; set; }
    }

    public class Self
    {
        public string Href { get; set; }
    }

    public class Gender
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class Faction
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class Race
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Key
    {
        public string Href { get; set; }
    }

    public class CharacterClass
    {
        public Key1 Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Key1
    {
        public string Href { get; set; }
    }

    public class ActiveSpec
    {
        public Key2 Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Key2
    {
        public string Href { get; set; }
    }

    public class Realm
    {
        public Key3 Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Slug { get; set; }
    }

    public class Key3
    {
        public string Href { get; set; }
    }

    public class Guild
    {
        public Key4 Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public Realm1 Realm { get; set; }
        public Faction1 Faction { get; set; }
    }

    public class Key4
    {
        public string Href { get; set; }
    }

    public class Realm1
    {
        public Key5 Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Slug { get; set; }
    }

    public class Key5
    {
        public string Href { get; set; }
    }

    public class Faction1
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class Achievements
    {
        public string Href { get; set; }
    }

    public class Titles
    {
        public string Href { get; set; }
    }

    public class PvpSummary
    {
        public string Href { get; set; }
    }

    public class Encounters
    {
        public string Href { get; set; }
    }

    public class Media
    {
        public string Href { get; set; }
    }

    public class Specializations
    {
        public string Href { get; set; }
    }

    public class Statistics
    {
        public string Href { get; set; }
    }

    public class MythicKeystoneProfile
    {
        public string Href { get; set; }
    }

    public class Equipment
    {
        public string Href { get; set; }
    }

    public class Appearance
    {
        public string Href { get; set; }
    }

    public class Collections
    {
        public string Href { get; set; }
    }

    public class Reputations
    {
        public string Href { get; set; }
    }

    public class Quests
    {
        public string Href { get; set; }
    }

    public class AchievementsStatistics
    {
        public string Href { get; set; }
    }

    public class Professions
    {
        public string Href { get; set; }
    }

    public class CovenantProgress
    {
        public ChosenCovenant ChosenCovenant { get; set; }
        public int RenownLevel { get; set; }
        public Soulbinds Soulbinds { get; set; }
    }

    public class ChosenCovenant
    {
        public Key6 Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Key6
    {
        public string Href { get; set; }
    }

    public class Soulbinds
    {
        public string Href { get; set; }
    }

}
