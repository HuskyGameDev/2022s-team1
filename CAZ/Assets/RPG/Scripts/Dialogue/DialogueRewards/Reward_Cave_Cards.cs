using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reward_Cave_Cards : DialogueTrigger_OnTouchCondition
{

    private void Start()
    {
        if (GameManager.instance.discovered_cave)
        {
            canInteract = false;
        }
    }

    public override IEnumerator CheckCondition()
    {
        GameManager.instance.player.GetComponent<PlayerController>().speed = 250; //I would unforce them eventually though.
        GameManager.instance.player.GetComponent<PlayerController>().canMove = true;

        StartCoroutine(DiscoverForestCards());

        yield return new WaitForEndOfFrame();
    }

    IEnumerator DiscoverForestCards()
    {

        GameObject.Find("CaveManager").GetComponent<CaveManager>().backwardsOverworldController.SetActive(false);

        GameManager.instance.discovered_cave = true;
        GameManager.instance.DeckbuilderButton.interactable = false;
        canInteract = false;
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Loomus"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Marayika"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Rorikz"));
        GameManager.instance.DeckbuilderButton.interactable = true;
        GameObject.Find("CaveManager").GetComponent<CaveManager>().backwardsOverworldController.SetActive(true);
    }

}

