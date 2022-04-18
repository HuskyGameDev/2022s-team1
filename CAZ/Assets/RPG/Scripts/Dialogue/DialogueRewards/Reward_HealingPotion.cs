using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_HealingPotion : DialogueTrigger_Condition
{
    public override IEnumerator CheckCondition()
    {
        Debug.Log("Completed Potion Dioalogue");
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Healing Potion"));
        GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
        this.gameObject.SetActive(false);

    }
}
