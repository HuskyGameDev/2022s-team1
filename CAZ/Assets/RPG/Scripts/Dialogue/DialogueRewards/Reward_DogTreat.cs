using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward_DogTreat : DialogueTrigger_Condition
{
    public Reward_Dodogger dodoggerDialogue;
    public DialogueBase dodoggerTreat;

    public override IEnumerator CheckCondition()
    {
        AudioManager.instance.Play("Item_Interact");
        GameManager.instance.holding = "DogTreat";
        GameManager.instance.holdingImage.sprite = this.gameObject.GetComponent<Image>().sprite;
        GameManager.instance.InventoryUI.SetActive(true);
        dodoggerDialogue.DB = dodoggerTreat;
        yield return new WaitForEndOfFrame();
    }
}
