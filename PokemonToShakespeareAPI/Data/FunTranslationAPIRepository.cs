using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static PokemonToShakespeareAPI.FunTranslationTranslation;

namespace PokemonToShakespeareAPI.Data
{
    public class FunTranslationAPIRepository : ITranslationRepository
    {
        private readonly HttpClient _httpClient;
        public FunTranslationAPIRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetToShakespeareTranslationAsync(string text)
        {
            string translationOutput = "";
            _httpClient.BaseAddress = new Uri("https://api.funtranslations.com/translate/");

            //HTTP Post
            var content = new FormUrlEncodedContent(new[]
                 {
                      new KeyValuePair<string, string>("text", text)
                });

            var responseTask = _httpClient.PostAsync("shakespeare.json/", content);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = await result.Content.ReadAsStringAsync();
                TranslatedText translation = JsonSerializer.Deserialize<TranslatedText>(readTask);
                if (translation.contents.translation.ToLower() == "shakespeare")
                {
                    translationOutput = translation.contents.translated;
                }
            }
            return translationOutput;
        }
    }
}