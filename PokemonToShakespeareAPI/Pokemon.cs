using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PokemonToShakespeareAPI
{
    [DataContract]
    public class Pokemon
    {
        [DataMember(Name = "id")]
        public int id { get; set; }
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
    [DataContract]
    public class Success
    {

        [DataMember(Name = "total")]
        public int total { get; set; }
    }

    [DataContract]
    public class Contents
    {

        [DataMember(Name = "translated")]
        public string translated { get; set; }

        [DataMember(Name = "text")]
        public string text { get; set; }

        [DataMember(Name = "translation")]
        public string translation { get; set; }
    }

    [DataContract]
    public class TranslatedText
    {

        [DataMember(Name = "success")]
        public Success success { get; set; }

        [DataMember(Name = "contents")]
        public Contents contents { get; set; }
    }
}
    