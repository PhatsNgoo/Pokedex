using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class Item : MonoBehaviour
{
    [SerializeField] Image thumbnail;
    [SerializeField] TextMeshProUGUI pokemonName;
    [SerializeField] TextMeshProUGUI pokemonBaseExperience;
    [SerializeField] TextMeshProUGUI pokemonHeight;
    [SerializeField] TextMeshProUGUI pokemonWeight;

    public void SetUpItem(Pokemon pokemonData)
    {
        pokemonName.text=pokemonData.Name;
        pokemonBaseExperience.text=pokemonData.BaseExperience.ToString();
        pokemonHeight.text=pokemonData.Height.ToString();
        pokemonWeight.text=pokemonData.Weight.ToString();
    }
}
