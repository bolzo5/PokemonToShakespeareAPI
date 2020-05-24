using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Configuration;
using PokemonToShakespeareAPI.Services;

namespace PokemonToShakespeareAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IConfiguration _config;
        public PokemonController(IConfiguration config)
        {
            _config = config;
        }
        // GET: api/Pokemon
        [HttpGet]
        public string Get()
        {
            string message= "Please call this API passing a pokemon name as parameter, example: pokemon/charizard";
            return message;
        }

        // GET: api/Pokemon/name
        [HttpGet("{Name}", Name = "Get")]
        public string Get(string Name)
        {
            string textLanguage=_config.GetValue<string>(
                "PokemonAPIOptions:Language");

            Task<string> task = getPokemonDescriptionAsync(Name.ToLower(),textLanguage);
            task.Wait();
            string name = task.Result;
            return name;
        }

        
        public async Task<string> getPokemonDescriptionAsync(string Name,string textLanguage)
        {

            int id = 0;
            string des = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon");
                //HTTP GET
                var responseTask = client.GetAsync("pokemon/" + Name);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //name = readTask;
                    Pokemon pk = JsonSerializer.Deserialize<Pokemon>(readTask);
                    id = pk.id;
                }
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
                //HTTP GET
                var responseTask = client.GetAsync("pokemon-species/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    //name = readTask;
                    PokemonSpecies species = JsonSerializer.Deserialize<PokemonSpecies>(readTask);

                    foreach (FlavorTextEntry entry in species.flavor_text_entries)
                    {
                        if (entry.language.name.ToLower() == textLanguage)
                        {
                            des = entry.flavor_text;
                        }
                    }
                }
            }
            //into service, clean string!
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.funtranslations.com/translate/");
                //HTTP Post

                var json = JsonSerializer.Serialize(des);


                var content = new FormUrlEncodedContent(new[]
                     {
                new KeyValuePair<string, string>("text", des)
            });

                var responseTask = client.PostAsync("shakespeare.json/", content);

                responseTask.Wait();


                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsStringAsync();
                    TranslatedText translation = JsonSerializer.Deserialize<TranslatedText>(readTask);
                    if (translation.contents.translation.ToLower() == "shakespeare")
                    {
                        des = translation.contents.translated;
                    }
                }
            }
            return des;
        }
    }
}
