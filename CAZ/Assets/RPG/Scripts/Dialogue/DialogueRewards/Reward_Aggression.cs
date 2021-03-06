using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Aggression : DialogueTrigger_Condition
{

    public DialogueBase kidConvoCarrotFound;

    private void Start()
    {
        if (GameManager.instance.holding == "Carrot") {
            DB = kidConvoCarrotFound;
        }
    }

    public override IEnumerator CheckCondition()
    {
        if (GameManager.instance.holding == "Carrot")
        {
            Debug.Log("Completed Carrot Dioalogue");
            GameManager.instance.InventoryUI.SetActive(false);
            GameManager.instance.holding = null;
            yield return StartCoroutine(GameManager.instance.DiscoverCard("Aggression"));
        }
    }
}
