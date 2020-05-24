using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PokemonToShakespeareAPI.Services;

namespace PokemonToShakespeareAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IConfiguration _config;
        private IPokemonService _pokemonService;

        public PokemonController(IConfiguration config, IPokemonService pokemonService)
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
        public IActionResult Get(string Name)
        {
            try
            {
                string textLanguage = _config.GetValue<string>(
                    "PokemonAPIOptions:Language");

                Task<Pokemon> task = _pokemonService.getPokemonFlavorTextTranslationAsync(Name.ToLower(), textLanguage);

                task.Wait();

                Pokemon result = task.Result;

                if (result.name == "" || result.description == "")
                {
                    return NotFound(Name + " not found ");
                } else if (result.description == "429")
                {
                    return StatusCode((int)System.Net.HttpStatusCode.TooManyRequests);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
