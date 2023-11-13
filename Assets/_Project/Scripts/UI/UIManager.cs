using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] List<Pokemon> pokemons=new List<Pokemon>();

    [SerializeField] List<Transform> scrollItems=new List<Transform>();


    public void AddPokemons(Pokemon pokemonData)
    {
        pokemons.Add(pokemonData);
    }
    public void SetUpPokemonList()
    {
        for(int i=0;i<pokemons.Count;i++)
        {
            scrollItems[i].GetComponent<Item>().SetUpItem(pokemons[i]);
        }
    }
}
