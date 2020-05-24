﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokemonToShakespeareAPI.Data
{
    public class PokeAPIRepository : IPokemonRepository
    {
        //public async Task<string> getPokemonDescriptionAsync(string Name, string textLanguage)
        public async Task<Pokemon> GetPokemonDescriptionAsync(Pokemon pokemon, string textLanguage)
        {
            string id = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon");
                //HTTP GET
                var responseTask = client.GetAsync("pokemon/" + pokemon.name);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //name = readTask;
                    PokeAPIPokemon pk = JsonSerializer.Deserialize<PokeAPIPokemon>(readTask);
                    id = pk.id;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
                //HTTP GET
                var responseTask2 = client.GetAsync("pokemon-species/" + id);
                responseTask2.Wait();

                var result2 = responseTask2.Result;
                if (result2.IsSuccessStatusCode)
                {
                    var readTask = await result2.Content.ReadAsStringAsync();
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
            }
            return pokemon;
        }
    }
}
