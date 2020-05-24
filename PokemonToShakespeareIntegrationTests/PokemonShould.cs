using PokemonToShakespeareAPI;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace PokemonToShakespeareIntegrationTests
{
    public class PokemonShould : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public PokemonShould(TestFixture fixture)
        {
            _client = fixture.Client;
        }
        [Fact]
        public async Task ShouldReturnPokemonAPIMessage()
        {
            // Arrange
            string message = "Please call this API passing a pokemon name as parameter, example: pokemon/charizard";
            var request = new HttpRequestMessage(
              HttpMethod.Get, "/pokemon/");
            // Act
            var response = await _client.SendAsync(request);
            // Assert
            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(message, content);
        }
        [Fact]
        public async Task ShouldReturnPokemonDescriptionTranslated()
        {
            // Arrange
            string nameOfPokemonToTranslate = "pikachu";
            var request = new HttpRequestMessage(
                HttpMethod.Get, "/pokemon/" + nameOfPokemonToTranslate);

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Pokemon responsePokemon = JsonSerializer.Deserialize<Pokemon>(content);
            Assert.Equal(nameOfPokemonToTranslate, responsePokemon.name);
            string description = "Its nature is to store up electricity.Forests whither aeries of pikachu liveth art dangerous, since the trees art so oft did strike by lightning.";
            Assert.Equal(description, responsePokemon.description);
        }
        [Fact]
        public async Task ShouldReturnPokemonDescriptionNotFound()
        {
            // Arrange
            string nameOfPokemonToTranslate = "pikaFhu";
            var request = new HttpRequestMessage(
                HttpMethod.Get, "/pokemon/" + nameOfPokemonToTranslate);

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(
                HttpStatusCode.NotFound,
                response.StatusCode);
        }
    }
}
