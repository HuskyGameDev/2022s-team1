using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_INT_Manager : MonoBehaviour
{
    public GameObject backwardsOverworldController;
    public GameObject bossTrigger;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.bossBattle = false;
        Debug.Log("Castle Loaded");
        AudioManager.instance.Play("Castle_INT_Overworld");
        AudioManager.instance.overworldSong = "Castle_INT_Overworld";

    }

    IEnumerator DiscoverCaveCards() {
        backwardsOverworldController.SetActive(false);

        GameManager.instance.DeckbuilderButton.interactable = false;
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Ashix"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Nocto"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Sulfu"));
        GameManager.instance.DeckbuilderButton.interactable = true;

        backwardsOverworldController.SetActive(true);
    }

}
