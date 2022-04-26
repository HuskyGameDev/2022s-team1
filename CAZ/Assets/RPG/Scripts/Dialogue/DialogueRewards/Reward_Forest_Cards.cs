using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reward_Forest_Cards : DialogueTrigger_OnTouchCondition
{

    private void Start()
    {
        if (GameManager.instance.discovered_forest)
        {
            canInteract = false;
        }
    }

    public override IEnumerator CheckCondition()
    {
        GameManager.instance.player.GetComponent<PlayerController>().speed = 250; //I would unforce them eventually though.
        GameManager.instance.player.GetComponent<PlayerController>().canMove = true;

        StartCoroutine(DiscoverVillageCards());

        yield return new WaitForEndOfFrame();
    }

    IEnumerator DiscoverVillageCards()
    {

        GameObject.Find("ForestManager").GetComponent<ForestManager>().backwardsOverworldController.SetActive(false);

        GameManager.instance.discovered_forest = true;
        GameManager.instance.DeckbuilderButton.interactable = false;
        canInteract = false;
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Zekeke"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Yamazuku"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Nindr"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Sleight of Hand"));
        GameManager.instance.DeckbuilderButton.interactable = true;
        GameObject.Find("ForestManager").GetComponent<ForestManager>().backwardsOverworldController.SetActive(true);
    }

}

