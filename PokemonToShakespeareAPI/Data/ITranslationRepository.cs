using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonToShakespeareAPI.Data
{
    public interface ITranslationRepository
    {
        public Task<string> GetToShakespeareTranslationAsync(string text);
    }
}
