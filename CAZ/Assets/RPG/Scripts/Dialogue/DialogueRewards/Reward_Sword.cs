using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward_Sword : DialogueTrigger_Condition
{
    public Reward_Shield shieldDialogue;
    public DialogueBase shieldSword;

    public override IEnumerator CheckCondition()
    {
        AudioManager.instance.Play("Item_Interact");
        GameManager.instance.holding = "Sword";
        GameManager.instance.holdingImage.sprite = this.gameObject.GetComponent<Image>().sprite;
        GameManager.instance.InventoryUI.SetActive(true);
        shieldDialogue.DB = shieldSword;
        yield return new WaitForEndOfFrame();
    }
}
