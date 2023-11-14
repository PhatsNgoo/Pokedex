using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] List<Pokemon> pokemons = new List<Pokemon>();

    [SerializeField] List<Transform> scrollItems = new List<Transform>();
    [SerializeField] Button previousPageBtn;
    [SerializeField] Button nextPageBtn;
    [SerializeField] ObjectPooler objectPooler;
    [SerializeField] public Transform scrollViewParent;

    private void Awake()
    {
    }
    private void Start()
    {

    }
    public void AddPokemons(Pokemon data)
    {
        pokemons.Add(data);
    }

    public void AddScollItems(Transform item)
    {
        scrollItems.Add(item);
    }
    public void SetUpPokemonList()
    {
        for (int i = 0; i < pokemons.Count; i++)
        {
            if(!scrollItems[i].gameObject.activeSelf)
            {
                scrollItems[i].gameObject.SetActive(true);
            }
            scrollItems[i].GetComponent<Item>().SetUpItem(pokemons[i]);
        }
        if (pokemons.Count < scrollItems.Count)
        {
            for (int i = pokemons.Count; i < scrollItems.Count; i++)
            {
                scrollItems[i].gameObject.SetActive(false);
            }
        }
    }
    public void SetUpPageButton(string next, string previous)
    {
        previousPageBtn.gameObject.SetActive(previous == "null" ? false : true);
        nextPageBtn.gameObject.SetActive(next == "null" ? false : true);
        previousPageBtn.onClick.RemoveAllListeners();
        nextPageBtn.onClick.RemoveAllListeners();
        previousPageBtn.onClick.AddListener(() => LoadPrevPage(previous));
        nextPageBtn.onClick.AddListener(() => LoadNextPage(next));
    }
    void LoadNextPage(string next)
    {
        if (!NetworkRequest.Instance.IsRequesting())
            NetworkRequest.Instance.RequestList(next);
    }
    void LoadPrevPage(string prev)
    {
        if (!NetworkRequest.Instance.IsRequesting())
            NetworkRequest.Instance.RequestList(prev);
    }
    public void ClearPokemons()
    {
        pokemons.Clear();
    }
}
