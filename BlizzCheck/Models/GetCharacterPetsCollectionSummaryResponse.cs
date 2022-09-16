using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlizzCheck.Models.Pets
{
    public class GetCharacterPetsCollectionSummaryResponse
    {
        [JsonPropertyName("_links")]
        public Links Links { get; set; }
        public Pet[] Pets { get; set; }
        public int UnlockedBattlePetSlots { get; set; }
    }

    public class Links
    {
        public Self Self { get; set; }
    }

    public class Self
    {
        public string Href { get; set; }
    }

    public class Pet
    {
        public Species Species { get; set; }
        public int Level { get; set; }
        public Quality Quality { get; set; }
        public Stats Stats { get; set; }
        public CreatureDisplay CreatureDisplay { get; set; }
        public int Id { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsActive { get; set; }
        public int ActiveSlot { get; set; }
        public string Name { get; set; }
    }

    public class Species
    {
        public Key Key { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Key
    {
        public string Href { get; set; }
    }

    public class Quality
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class Stats
    {
        public int BreedId { get; set; }
        public int Health { get; set; }
        public int Power { get; set; }
        public int Speed { get; set; }
    }

    public class CreatureDisplay
    {
        public Key Key { get; set; }
        public int Id { get; set; }
    }

}
