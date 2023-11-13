using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkRequest : Singleton<NetworkRequest>
{
    [SerializeField] string absoluteURL;
    [SerializeField] string apiURL;
    PokemonList pokemonList;

    private void Start() {
        RequestList();
    }

    private void RequestList()
    {
        StartCoroutine(GetAllPokemons());
    }
    IEnumerator GetAllPokemons()
    {
        UnityWebRequest uwr=UnityWebRequest.Get(apiURL);

        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError){
            Debug.LogError("Error while sending request:" + uwr.error);
        }
        else
        {
            Debug.LogError("Received response: "+uwr.downloadHandler.text);
            pokemonList=JsonConvert.DeserializeObject<PokemonList>(uwr.downloadHandler.text);
        }
        
        foreach(var item in pokemonList.Results)
        {
            Debug.LogError(absoluteURL+item.Url.AbsolutePath);
            StartCoroutine(GetPokemon(absoluteURL+item.Url.AbsolutePath));
        }
    }

    IEnumerator GetPokemon(string targetURL)
    {
        UnityWebRequest uwr=UnityWebRequest.Get(targetURL);

        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError){
            Debug.LogError("Error while sending request:" + uwr.error);
        }
        else
        {
            UIManager.Instance.AddPokemons(JsonConvert.DeserializeObject<Pokemon>(uwr.downloadHandler.text));
            // Pokemon test=JsonConvert.DeserializeObject<Pokemon>(uwr.downloadHandler.text);
            // Debug.LogError(test.Name);
            // Debug.LogError(test.Height.ToString());
            // UIManager.Instance.A
        }
    }
}