using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward_Map : DialogueTrigger_Condition
{
    public Reward_Revive ghostDialogue;
    public DialogueBase reviveDB;

    public override IEnumerator CheckCondition()
    {
        AudioManager.instance.Play("Item_Interact");
        GameManager.instance.holding = "Map";
        GameManager.instance.holdingImage.sprite = this.gameObject.GetComponent<Image>().sprite;
        GameManager.instance.InventoryUI.SetActive(true);
        ghostDialogue.DB = reviveDB;
        yield return new WaitForEndOfFrame();
    }
}
