using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Dodogger : DialogueTrigger_Condition
{
    public override IEnumerator CheckCondition()
    {
        if (GameManager.instance.holding == "DogTreat")
        {
            Debug.Log("Completed Dodogger Dioalogue");
            GameManager.instance.InventoryUI.SetActive(false);
            GameManager.instance.holding = null;
            yield return StartCoroutine(GameManager.instance.DiscoverCard("Dodogger"));
            GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
