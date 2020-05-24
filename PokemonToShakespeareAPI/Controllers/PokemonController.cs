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
//using PokemonToShakespeareAPI.Services;
using System.Text.RegularExpressions;
using PokemonToShakespeareAPI.Data;
using PokemonToShakespeareAPI.Services;

namespace PokemonToShakespeareAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IConfiguration _config;
        private IPokemonService _pokemonService;
        //public PokemonController(IConfiguration config)
        //{
        //    _config = config;
        //}
    
        public PokemonController(IConfiguration config,IPokemonService pokemonService)
        {
            _config = config;
            _pokemonService = pokemonService;
        }
        // GET: api/Pokemon
        [HttpGet]
        public string Get()
        {
            string message = "Please call this API passing a pokemon name as parameter, example: pokemon/charizard";
            return message;
        }

        // GET: api/Pokemon/name
        [HttpGet("{Name}", Name = "Get")]
        public string Get(string Name)
        {
            string textLanguage = _config.GetValue<string>(
                "PokemonAPIOptions:Language");

            Task<Pokemon> task = _pokemonService.getPokemonFlavorTextTranslationAsync(Name.ToLower(), textLanguage);

            task.Wait();
            string name = task.Result.description;
            return name;
        }


        
    }
}
