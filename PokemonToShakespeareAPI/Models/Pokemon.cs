using System.Runtime.Serialization;

namespace PokemonToShakespeareAPI
{
    public class Pokemon
    {
        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "description")]
        public string description { get; set; }
        public Pokemon(string pokemonName, string pokemonDescription = "")
        {
            name = pokemonName;
            description = pokemonDescription;
        }
        public Pokemon()
        {
            name = "";
            description = "";
        }

    }
}
