using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_ShadowStrike : DialogueTrigger_Condition
{

    public DialogueBase shadowWater;

    private void Start()
    {
        if (GameManager.instance.holding == "Water")
        {
            DB = shadowWater;
        }
    }

    public override IEnumerator CheckCondition()
    {
        if (GameManager.instance.holding == "Water")
        {
            Debug.Log("Completed ShadowStrike Dioalogue");
            GameManager.instance.InventoryUI.SetActive(false);
            GameManager.instance.holding = null;
            yield return StartCoroutine(GameManager.instance.DiscoverCard("Shadow Strike"));
            GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
