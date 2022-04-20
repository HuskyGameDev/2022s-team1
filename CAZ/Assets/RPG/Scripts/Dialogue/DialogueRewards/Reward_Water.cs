using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward_Water : DialogueTrigger_Condition
{
    public Reward_ShadowStrike shadowStrikeDialogue;
    public DialogueBase shadowWater;
    public override IEnumerator CheckCondition()
    {
        AudioManager.instance.Play("Item_Interact");
        GameManager.instance.holding = "Water";
        GameManager.instance.holdingImage.sprite = this.gameObject.GetComponent<Image>().sprite;
        GameManager.instance.InventoryUI.SetActive(true);
        shadowStrikeDialogue.DB = shadowWater;
        yield return new WaitForEndOfFrame();
    }
}
