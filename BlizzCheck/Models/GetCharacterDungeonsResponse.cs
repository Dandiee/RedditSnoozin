using System.Text.Json.Serialization;

namespace BlizzCheck.Models
{

    public class GetCharacterDungeonsResponse
    {
        [JsonPropertyName("_links")]
        public Links Links { get; set; }
        public ExpansionInfo[] Expansions { get; set; }
    }

    public class Links
    {
        public Self Self { get; set; }
    }

    public class Self
    {
        public string Href { get; set; }
    }

    public class ExpansionInfo
    {
        public ExpansionDetails Expansion { get; set; }
        public InstanceInfo[] Instances { get; set; }
    }

    public class ExpansionDetails
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Key
    {
        public string Href { get; set; }
    }

    public class InstanceInfo
    {
        public InstanceDetails Instance { get; set; }
        public Mode[] Modes { get; set; }
    }

    public class InstanceDetails
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

  

    public class Mode
    {
        public Difficulty Difficulty { get; set; }
        public Status Status { get; set; }
        public Progress Progress { get; set; }
    }

    public class Difficulty
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class Status
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class Progress
    {
        public int CompletedCount { get; set; }
        public int TotalCount { get; set; }
        public EncounterInfo[] Encounters { get; set; }
    }

    public class EncounterInfo
    {
        public EncounterDetails Encounter { get; set; }
        public int CompletedCount { get; set; }
        public long LastKillTimestamp { get; set; }
    }

    public class EncounterDetails
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

}
