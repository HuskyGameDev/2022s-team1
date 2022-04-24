using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reward_Castle_Cards : DialogueTrigger_OnTouchCondition
{

    private void Start()
    {
        if (GameManager.instance.discovered_castle){
            canInteract = false;
        }
    }

    public override IEnumerator CheckCondition()
    {
        GameManager.instance.player.GetComponent<PlayerController>().speed = 250; //I would unforce them eventually though.
        GameManager.instance.player.GetComponent<PlayerController>().canMove = true;
        
        StartCoroutine(DiscoverCaveCards());

        yield return new WaitForEndOfFrame();
    }

    IEnumerator DiscoverCaveCards() {
        
        GameObject.Find("CastleManager").GetComponent<CastleManager>().backwardsOverworldController.SetActive(false);

        GameManager.instance.discovered_castle = true;
        GameManager.instance.DeckbuilderButton.interactable = false;
        canInteract = false;
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Ashix"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Nocto"));
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Sulfu"));
        GameManager.instance.DeckbuilderButton.interactable = true;
        GameObject.Find("CastleManager").GetComponent<CastleManager>().backwardsOverworldController.SetActive(true);
    }

}
