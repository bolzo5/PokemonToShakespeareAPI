using System.Threading.Tasks;

namespace PokemonToShakespeareAPI.Services
{
    public interface IPokemonService
    {
        public Task<Pokemon> getPokemonFlavorTextTranslationAsync(string name, string textLanguage);
    }
}
