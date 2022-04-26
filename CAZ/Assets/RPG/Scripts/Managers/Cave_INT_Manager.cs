using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave_INT_Manager : MonoBehaviour
{
    public GameObject backwardsOverworldController;
    public GameObject bossTrigger;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.bossBattle = false;

        if (GameManager.instance.discovered_castle)
        {
            bossTrigger.SetActive(false);
        }

        Debug.Log("Cave Loaded");
        AudioManager.instance.Stop("Main_Theme");
        AudioManager.instance.Play("Cave_Overworld");
        AudioManager.instance.overworldSong = "Cave_Overworld";
        if (!GameManager.instance.discovered_cave)
        {
            GameManager.instance.discovered_cave = true;
            GameManager.instance.deckMax = 30;
            GameManager.instance.battleHp = 30;
            StartCoroutine(DiscoverCaveCards());
        }

        PlayerController player = GameManager.instance.player.GetComponent<PlayerController>();
        if (!player.battlePrimed)
            player.battleCooldownTimer = player.battleCooldownTime;

    }

    IEnumerator DiscoverCaveCards()
    {
        backwardsOverworldController.SetActive(false);

        GameManager.instance.DeckbuilderButton.interactable = false;
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Loomus"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Marayika"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Rorikz"));
        GameManager.instance.DeckbuilderButton.interactable = true;

        backwardsOverworldController.SetActive(true);
    }

}
