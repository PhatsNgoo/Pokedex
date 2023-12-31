using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
public class Item : MonoBehaviour
{
    [SerializeField] Image thumbnail;
    [SerializeField] TextMeshProUGUI pokemonName;
    [SerializeField] TextMeshProUGUI pokemonBaseExperience;
    [SerializeField] TextMeshProUGUI pokemonHeight;
    [SerializeField] TextMeshProUGUI pokemonWeight;
    [SerializeField] Sprite dummySprite;

    private void Start()
    {
    }
    public void SetUpItem(Pokemon pokemonData)
    {
        pokemonName.text = pokemonData.Name;
        pokemonBaseExperience.text = pokemonData.BaseExperience.ToString();
        pokemonHeight.text = pokemonData.Height.ToString();
        pokemonWeight.text = pokemonData.Weight.ToString();
        StartCoroutine(GetThumbnail(pokemonData.Sprites.FrontDefault?.ToString()));
    }

    IEnumerator GetThumbnail(string uri)
    {
        if(uri==null || uri=="null"|| uri=="Null")
        {
            thumbnail.sprite = dummySprite;
            yield break;
        }
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(uri);
        uwr.SendWebRequest();
        yield return new WaitUntil(() => uwr.isDone);

        if (uwr.isNetworkError)
        {
            Debug.LogError(uri);
            Debug.LogError("Error while sending request:" + uwr.error);
            thumbnail.sprite = dummySprite;
        }
        else
        {
            try
            {
                Texture2D texture = ((DownloadHandlerTexture)uwr.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                thumbnail.sprite = sprite;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                Debug.Log(uri);
            }
        }

    }
}
