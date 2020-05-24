using Moq;
using Moq.Protected;
using PokemonToShakespeareAPI.Data;
using PokemonToShakespeareAPI.Services;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PokemonToShakeSpeareTests
{
    public class PokemonServiceShould
    {
        [Fact]
        public async void ShouldReturnPokemonDescriptionTranslated()
        {
            //arrange
            var handlerMockPokeAPI = new Mock<HttpMessageHandler>();
            var responsePokeAPI = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""flavor_text_entries"":[{""flavor_text"":""Its nature is to store up electricity. Forests\nwhere nests of Pikachu live are dangerous,\nsince the trees are so often struck by lightning."",""language"":{""name"":""en"",""url"":""https://pokeapi.co/api/v2/language/9/""},""version"":{""name"":""ultra-sun"",""url"":""https://pokeapi.co/api/v2/version/29/""}}]}"),
            };

            handlerMockPokeAPI
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(responsePokeAPI);
            var httpClientPokeAPI = new HttpClient(handlerMockPokeAPI.Object);

            var handlerMockFunTranslationAPI = new Mock<HttpMessageHandler>();
            var responseFunTranslationAPI = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""success"": {""total"": 1},""contents"": {""translated"": ""Its nature is to store up electricity.Forests whither aeries of pikachu liveth art dangerous, since the trees art so oft did strike by lightning."",""text"": ""Its nature is to store up electricity.Forests\nwhere nests of Pikachu live are dangerous,\nsince the trees are so often struck by lightning."",""translation"": ""shakespeare""}}"),
            };

            handlerMockFunTranslationAPI
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(responseFunTranslationAPI);
            var httpClientFunTranslationAPI = new HttpClient(handlerMockFunTranslationAPI.Object);

            var pokeApi = new PokeAPIRepository(httpClientPokeAPI);
            var funTranslationAPI = new FunTranslationAPIRepository(httpClientFunTranslationAPI);

            //act
            string nameOfPokemonToTranslate = "pikachu";
            var pokemonService = new PokemonService(pokeApi, funTranslationAPI);
            var pokemon = await pokemonService.getPokemonFlavorTextTranslationAsync(nameOfPokemonToTranslate, "en");

            //assert
            Assert.NotNull(pokemon);
            handlerMockPokeAPI.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
            handlerMockFunTranslationAPI.Protected().Verify(
           "SendAsync",
           Times.Exactly(1),
           ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
           ItExpr.IsAny<CancellationToken>());

            Assert.Equal(nameOfPokemonToTranslate, pokemon.name);
            string description = "Its nature is to store up electricity.Forests whither aeries of pikachu liveth art dangerous, since the trees art so oft did strike by lightning.";
            Assert.Equal(description, pokemon.description);
        }
        [Fact]
        public async void ShouldReturnEmptyDescription()
        {
            //arrange
            var handlerMockPokeAPI = new Mock<HttpMessageHandler>();
            var responsePokeAPI = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            };

            handlerMockPokeAPI
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(responsePokeAPI);
            var httpClientPokeAPI = new HttpClient(handlerMockPokeAPI.Object);

            var handlerMockFunTranslationAPI = new Mock<HttpMessageHandler>();
            var responseFunTranslationAPI = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
            };

            handlerMockFunTranslationAPI
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(responseFunTranslationAPI);
            var httpClientFunTranslationAPI = new HttpClient(handlerMockFunTranslationAPI.Object);

            var pokeApi = new PokeAPIRepository(httpClientPokeAPI);
            var funTranslationAPI = new FunTranslationAPIRepository(httpClientFunTranslationAPI);

            //act
            string nameOfPokemonToTranslate = "pikachu";
            var pokemonService = new PokemonService(pokeApi, funTranslationAPI);
            var pokemon = await pokemonService.getPokemonFlavorTextTranslationAsync(nameOfPokemonToTranslate, "en");

            //assert
            Assert.NotNull(pokemon);
            handlerMockPokeAPI.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());

            Assert.Equal(nameOfPokemonToTranslate, pokemon.name);
            Assert.Equal("", pokemon.description);
        }
        [Fact]
        public async void ShouldReturnTooManyStatusCode()
        {
            //arrange
            var handlerMockPokeAPI = new Mock<HttpMessageHandler>();
            var responsePokeAPI = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(@"{""flavor_text_entries"":[{""flavor_text"":""Its nature is to store up electricity. Forests\nwhere nests of Pikachu live are dangerous,\nsince the trees are so often struck by lightning."",""language"":{""name"":""en"",""url"":""https://pokeapi.co/api/v2/language/9/""},""version"":{""name"":""ultra-sun"",""url"":""https://pokeapi.co/api/v2/version/29/""}}]}"),
            };

            handlerMockPokeAPI
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(responsePokeAPI);
            var httpClientPokeAPI = new HttpClient(handlerMockPokeAPI.Object);

            var handlerMockFunTranslationAPI = new Mock<HttpMessageHandler>();
            var responseFunTranslationAPI = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.TooManyRequests,
            };

            handlerMockFunTranslationAPI
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(responseFunTranslationAPI);
            var httpClientFunTranslationAPI = new HttpClient(handlerMockFunTranslationAPI.Object);

            var pokeApi = new PokeAPIRepository(httpClientPokeAPI);
            var funTranslationAPI = new FunTranslationAPIRepository(httpClientFunTranslationAPI);

            //act
            string nameOfPokemonToTranslate = "pikachu";
            var pokemonService = new PokemonService(pokeApi, funTranslationAPI);
            var pokemon = await pokemonService.getPokemonFlavorTextTranslationAsync(nameOfPokemonToTranslate, "en");

            //assert
            Assert.NotNull(pokemon);
            handlerMockPokeAPI.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
               ItExpr.IsAny<CancellationToken>());
            handlerMockFunTranslationAPI.Protected().Verify(
           "SendAsync",
           Times.Exactly(1),
           ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post),
           ItExpr.IsAny<CancellationToken>());

            Assert.Equal(HttpStatusCode.TooManyRequests.GetHashCode().ToString(), pokemon.description);
        }

    }
}
