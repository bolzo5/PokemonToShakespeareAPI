using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonToShakespeareAPI.Data
{
    public class PokeAPIRepository : IPokemonRepository
    {
        public HttpClient _httpClient;
        public PokeAPIRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Pokemon> GetPokemonDescriptionAsync(Pokemon pokemon, string textLanguage)
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
            }
            //_httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
            //HTTP GET
            var responseTask = _httpClient.GetAsync("pokemon-species/" + pokemon.name);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = await result.Content.ReadAsStringAsync();
                PokemonSpecies species = JsonSerializer.Deserialize<PokemonSpecies>(readTask);

                foreach (FlavorTextEntry entry in species.flavor_text_entries)
                {
                    if (entry.language.name.ToLower() == textLanguage)
                    {
                        //retrieve latest game version containing the pokemon id of the flavour text
                        pokemon.description = entry.flavor_text;
                        break;
                    }
                }
            }

            return pokemon;
        }
    }
}
