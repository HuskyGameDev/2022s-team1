using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Jyni : DialogueTrigger_Condition
{
    public override IEnumerator CheckCondition()
    {
        Debug.Log("Completed Jyni Dioalogue");
        yield return StartCoroutine(GameManager.instance.DiscoverCard("Jyni"));
        GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
        this.gameObject.SetActive(false);

    }
}
