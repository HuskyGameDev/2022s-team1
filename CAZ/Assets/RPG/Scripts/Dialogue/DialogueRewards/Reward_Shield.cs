using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Shield : DialogueTrigger_Condition
{
    public DialogueBase shieldSword;

    private void Start()
    {
        if (GameManager.instance.holding == "Sword")
        {
            DB = shieldSword;
        }
    }

    public override IEnumerator CheckCondition()
    {
        if (GameManager.instance.holding == "Sword")
        {
            Debug.Log("Completed Soldier Dioalogue");
            GameManager.instance.InventoryUI.SetActive(false);
            GameManager.instance.holding = null;
            yield return StartCoroutine(GameManager.instance.DiscoverCard("Shield"));
            GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
