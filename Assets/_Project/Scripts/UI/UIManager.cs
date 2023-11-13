using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] List<Pokemon> pokemons=new List<Pokemon>();

    [SerializeField] List<Transform> scrollItems=new List<Transform>();
    [SerializeField] Button previousPageBtn;
    [SerializeField] Button nextPageBtn;

    private void Awake() {
        previousPageBtn.onClick.RemoveAllListeners();
        nextPageBtn.onClick.RemoveAllListeners();
    }
    private void Start() {
        
    }
    public void AddPokemons(Pokemon data)
    {
        pokemons.Add(data);
    }
    public void SetUpPokemonList()
    {
        for(int i=0;i<pokemons.Count;i++)
        {
            scrollItems[i].GetComponent<Item>().SetUpItem(pokemons[i]);
        }
    }
    public List<Pokemon> GetPokemonsToDisplay()
    {
        return pokemons;
    }
    public void SetUpPageButton(string next,string previous)
    {
        previousPageBtn.onClick.AddListener(()=>NetworkRequest.Instance.RequestList(previous));
        nextPageBtn.onClick.AddListener(()=>NetworkRequest.Instance.RequestList(next));
    }
}
