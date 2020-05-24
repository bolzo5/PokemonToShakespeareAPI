using System;

namespace PokemonToShakespeareAPI
{
    public class Pokemon
    {
        public string name { get; set; }
        public string description { get; set; }
        public Pokemon(string pokemonName, string pokemonDescription = "")
        {
            name = pokemonName;
            description = pokemonDescription;
        }

    }
}
