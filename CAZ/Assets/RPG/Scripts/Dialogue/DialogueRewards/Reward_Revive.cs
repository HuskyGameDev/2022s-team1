using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Revive : DialogueTrigger_Condition
{
    public GameObject ghost2;

    public DialogueBase reviveDB;

    private void Start()
    {
        if (GameManager.instance.holding == "Map")
        {
            DB = reviveDB;
        }
    }

    public override IEnumerator CheckCondition()
    {
        if (GameManager.instance.holding == "Map")
        {
            Debug.Log("Completed Revive Dioalogue");
            GameManager.instance.InventoryUI.SetActive(false);
            GameManager.instance.holding = null;
            yield return StartCoroutine(GameManager.instance.DiscoverCard("Revive"));
            ghost2.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
