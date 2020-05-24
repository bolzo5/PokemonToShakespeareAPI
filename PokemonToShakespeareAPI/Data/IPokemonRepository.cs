using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonToShakespeareAPI.Data
{
    public interface IPokemonRepository
    {
        public Task<Pokemon> GetPokemonDescriptionAsync(Pokemon pokemon, string textLanguage);
    }
}
