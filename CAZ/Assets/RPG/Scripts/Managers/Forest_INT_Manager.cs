using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_INT_Manager : MonoBehaviour
{
    public GameObject backwardsOverworldController;
    public GameObject bossTrigger;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.bossBattle = false;

        if (GameManager.instance.discovered_cave)
        {
            bossTrigger.SetActive(false);
        }

        Debug.Log("Forest Loaded");
        AudioManager.instance.Stop("Main_Theme");
        AudioManager.instance.Play("Forest_Overworld");
        AudioManager.instance.overworldSong = "Forest_Overworld";
        if (!GameManager.instance.discovered_forest)
        {
            GameManager.instance.discovered_forest = true;
            GameManager.instance.deckMax = 20;
            GameManager.instance.battleHp = 25;
            StartCoroutine(DiscoverForestCards());
        }

        PlayerController player = GameManager.instance.player.GetComponent<PlayerController>();
        if (!player.battlePrimed)
            player.battleCooldownTimer = player.battleCooldownTime;

    }

    IEnumerator DiscoverForestCards()
    {
        backwardsOverworldController.SetActive(false);

        GameManager.instance.DeckbuilderButton.interactable = false;
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Zekeke"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Yamazuku"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Nindr"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Sleight of Hand"));
        GameManager.instance.DeckbuilderButton.interactable = true;

        backwardsOverworldController.SetActive(true);
    }

}
