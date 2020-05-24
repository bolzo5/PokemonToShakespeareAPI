using PokemonToShakespeareAPI.Data;
using System.Threading.Tasks;

namespace PokemonToShakespeareAPI.Services
{
    public class PokemonService : IPokemonService
    {
        private IPokemonRepository _pokemonRepository;
        private ITranslationRepository _translationRepository;
        public PokemonService(IPokemonRepository pokemonRepository, ITranslationRepository translationRepository)
        {
            _pokemonRepository = pokemonRepository;
            _translationRepository = translationRepository;
        }
        public async Task<Pokemon> getPokemonFlavorTextTranslationAsync(string pokemonName, string textLanguage)
        {
            Pokemon pokemonToTranslate = new Pokemon(pokemonName);
            if (pokemonName != "")
            {
                //GET pokemon description from its name
                pokemonToTranslate = await _pokemonRepository.GetPokemonDescriptionAsync( pokemonToTranslate, textLanguage);
                //translate the description into shakespearian english
                if (pokemonToTranslate.description != "")
                {
                    pokemonToTranslate.description = await _translationRepository.GetToShakespeareTranslationAsync(pokemonToTranslate.description);
                }

            }
            return pokemonToTranslate;
        }
    }
}
