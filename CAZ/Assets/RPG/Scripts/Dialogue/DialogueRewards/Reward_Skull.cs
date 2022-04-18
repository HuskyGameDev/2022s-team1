using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward_Skull : DialogueTrigger_Condition
{
    public Reward_Sacrifice sacrificeDialogue;
    public DialogueBase cultistSkull;
    public override IEnumerator CheckCondition()
    {
        AudioManager.instance.Play("Item_Interact");
        GameManager.instance.holding = "Skull";
        GameManager.instance.holdingImage.sprite = this.gameObject.GetComponent<Image>().sprite;
        GameManager.instance.InventoryUI.SetActive(true);
        sacrificeDialogue.DB = cultistSkull;
        yield return new WaitForEndOfFrame();
    }
}
