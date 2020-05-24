using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonToShakespeareAPI.Services
{
    public interface IPokemonService
    {
        public Task<Pokemon> getPokemonFlavorTextTranslationAsync(string name, string textLanguage);
    }
}
