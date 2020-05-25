# PokemonToShakespeareAPI

A .Net Core 3.1 Web API that translates the pokedex entries of your favourites pokemon into Shakespearean english! 

## Getting Started & Prerequisites

Either clone using git or download the project.  
Set up Docker to run .NET core applications  
[DOCKER](https://docs.docker.com/)


### Installing & Running

Navigate to the project folder, the one with the .sln and dockerimage files.  
Run the following Docker CLI command to build the application image.  
```  
	docker build -t pokemontoshakespeareapi .
```  
Then start an instance of the image, running on port 32768:80
```  
	docker run --name pokemontoshakespeareapi --rm -it -p 32768:80 pokemontoshakespeareapi
```  
	
Finally you can try the endpoints at:
``` 
	http://localhost:32768/pokemon/
``` 
You should get a message about the API.  
If you add the name of a pokemon of your choice like:  
``` 
	http://localhost:32768/pokemon/bulbasaur
``` 
The response will contain the name and the description of that pokemon found in a pokedex ( for pokemon appearing in multiple games, the latest one will be shown).  
The description returned has been previously processed and translated into shakespearean english.  
Example of the previous call:  
``` 
	{"name":"bulbasaur","description":"Bulbasaur can beest seen napping in bright sunlight. Thither is a seed on its back. By soaking up the travelling lamp’s rays,  the seed grows progressively larger."}  
``` 
	
Please remember that the free tier of FunTranslation API used has a limit of 60 calls a day and no more than 5 an hour.  

## Running the tests
Included in the solution there are two test project, one for integration testing and one for unit testing.  
### Integration tests:
 Testing the generic GET, and both success/fail of the PokemonController  
  * ShouldReturnPokemonAPIMessage  
 * ShouldReturnPokemonDescriptionTranslated  
 * ShouldReturnPokemonDescriptionNotFound

### Unit Tests: 
  Testing a correct response from the PokemonService and two failure modes  
 * ShouldReturnPokemonDescriptionTranslated  
 * ShouldReturnEmptyDescription  
 * ShouldReturnTooManyStatusCode  



## Contributing  

Pull requests and open issue are welcome.  
Please feel free to help or comment so that I can learn how to improve this project.  

## Authors  

Andrea Bolzoni  

## License
[MIT](https://choosealicense.com/licenses/mit/)

## Acknowledgments

https://pokeapi.co/  
https://funtranslations.com/
