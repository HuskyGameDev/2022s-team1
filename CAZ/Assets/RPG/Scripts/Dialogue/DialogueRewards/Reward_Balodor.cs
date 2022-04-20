using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Balodor : DialogueTrigger_Condition
{
    public override IEnumerator CheckCondition()
    {
        Debug.Log("Completed Ventus Dioalogue");
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Balodor"));
        GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
        this.gameObject.SetActive(false);

    }
}
