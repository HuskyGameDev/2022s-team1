using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Hoasteban : DialogueTrigger_Condition
{

    public DialogueBase hoastebanPuzzleCompleteDB;

    private void Start()
    {
        if (GameManager.instance.holding == "Puzzle")
        {
            DB = hoastebanPuzzleCompleteDB;
        }
    }

    public override IEnumerator CheckCondition()
    {
        if (GameManager.instance.holding == "Puzzle")
        {
            Debug.Log("Completed Hoasteban Dioalogue");
            GameManager.instance.InventoryUI.SetActive(false);
            GameManager.instance.holding = null;
            yield return StartCoroutine(GameManager.instance.DiscoverCard("Hoasteban"));
            GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
