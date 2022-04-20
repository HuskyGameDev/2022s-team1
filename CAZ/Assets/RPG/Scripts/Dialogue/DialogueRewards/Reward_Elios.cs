using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Elios : DialogueTrigger_Condition
{
    public override IEnumerator CheckCondition()
    {
        Debug.Log("Completed Elios Dioalogue");
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Elios"));
        GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
        this.gameObject.SetActive(false);


    }
}
