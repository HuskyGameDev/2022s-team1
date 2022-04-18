using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Cave Loaded");
        if (!GameManager.instance.discovered_cave) {
            GameManager.instance.discovered_cave = true;
            GameManager.instance.deckMax = 30;
            GameManager.instance.battleHp = 30;
            AudioManager.instance.Play("Cave_Overworld");
            AudioManager.instance.overworldSong = "Cave_Overworld";
            StartCoroutine(DiscoverCaveCards());
        }
    }

    IEnumerator DiscoverCaveCards() {

        GameManager.instance.DeckbuilderButton.interactable = false;
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Loomus"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Marayika"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Rorikz"));
        GameManager.instance.DeckbuilderButton.interactable = true;


    }

}
