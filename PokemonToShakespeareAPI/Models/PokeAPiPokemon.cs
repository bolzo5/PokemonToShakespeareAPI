using System;
using System.Runtime.Serialization;

namespace PokemonToShakespeareAPI
{
    [DataContract]
    public class PokeAPIPokemon
    {
        [DataMember(Name = "id")]
        public string id { get; set; }
    }
    [DataContract]
    public class Language
    {
        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "url")]
        public string url { get; set; }

    }

    [DataContract]
    public class FlavorTextEntry
    {
        [DataMember(Name = "flavor_text")]
        public string flavor_text { get; set; }
        [DataMember(Name = "language")]
        public Language language { get; set; }

        [DataMember(Name = "version")]
        public Version version { get; set; }
      
    }
    [DataContract]
    public class PokemonSpecies
    {
        [DataMember(Name = "flavor_text_entries")]
        public FlavorTextEntry[] flavor_text_entries { get; set; }
      
    }

}
    