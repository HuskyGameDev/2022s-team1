using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Forest Loaded");
        if (!GameManager.instance.discovered_forest) {
            GameManager.instance.discovered_forest = true;
            GameManager.instance.deckMax = 20;
            GameManager.instance.battleHp = 25;
            AudioManager.instance.Play("Forest_Overworld");
            AudioManager.instance.overworldSong = "Forest_Overworld";
            StartCoroutine(DiscoverForestCards());
        }
    }

    IEnumerator DiscoverForestCards() {

        GameManager.instance.DeckbuilderButton.interactable = false;
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Zekeke"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Yamazuku"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Nindr"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Sleight of Hand"));
        GameManager.instance.DeckbuilderButton.interactable = true;


    }

}
