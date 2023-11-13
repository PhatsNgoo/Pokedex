using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkRequest : Singleton<NetworkRequest>
{
    [SerializeField] string apiURL;
    [SerializeField] float amountPerPage;
    PokemonList pokemonList;

    private void Start() {
        RequestList(NetworkConfig.ABSOLUTE_URL+NetworkConfig.POKEMON_URI+"?limit="+amountPerPage);
    }

    public void RequestList(string URL)
    {
        StartCoroutine(GetPokemons(URL));
    }
    IEnumerator GetPokemons(string URL)
    {
        UnityWebRequest uwr=UnityWebRequest.Get(URL);
        yield return uwr.SendWebRequest();

        if(uwr.isNetworkError){
            Debug.LogError("Error while sending request:" + uwr.error);
        }
        else
        {
            pokemonList=JsonConvert.DeserializeObject<PokemonList>(uwr.downloadHandler.text);
            StartCoroutine(GetPokemonsToDisplay());
            UIManager.Instance.SetUpPageButton(pokemonList.Next!=null?pokemonList.Next.ToString():"null",pokemonList.Previous!=null?pokemonList.Previous.ToString():"null");
        }
        
    }
    IEnumerator GetPokemonsToDisplay()
    {
        List<UnityWebRequestAsyncOperation> uwrs=new List<UnityWebRequestAsyncOperation>();

        foreach(var item in pokemonList.Results)
        {
            UnityWebRequest uwr=UnityWebRequest.Get(NetworkConfig.ABSOLUTE_URL+item.Url.AbsolutePath);
            uwrs.Add(uwr.SendWebRequest());
        }

        yield return new WaitUntil(()=>uwrs.All(x=>x.isDone));
        
        foreach(var item in uwrs)
        {
            UIManager.Instance.AddPokemons(JsonConvert.DeserializeObject<Pokemon>(item.webRequest.downloadHandler.text));
        }
        UIManager.Instance.SetUpPokemonList();
    }
}