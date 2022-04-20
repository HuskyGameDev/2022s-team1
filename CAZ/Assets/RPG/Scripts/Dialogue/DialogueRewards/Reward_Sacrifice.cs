using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Sacrifice : DialogueTrigger_Condition
{

    public DialogueBase cultistSkull;

    private void Start()
    {
        if (GameManager.instance.holding == "Skull")
        {
            DB = cultistSkull;
        }
    }

    public override IEnumerator CheckCondition()
    {
        if (GameManager.instance.holding == "Skull")
        {
            Debug.Log("Completed Cultist Dioalogue");
            GameManager.instance.InventoryUI.SetActive(false);
            GameManager.instance.holding = null;
            yield return StartCoroutine(GameManager.instance.DiscoverCard("Sacrifice"));
            GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
