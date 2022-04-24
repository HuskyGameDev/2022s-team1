using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reward_Water : DialogueTrigger_Condition
{
    public Reward_ShadowStrike shadowStrikeDialogue;
    public DialogueBase shadowWater;

    public DialogueBase haveWater;

    public override IEnumerator CheckCondition()
    {
        if (!(GameManager.instance.holding == "Water" || GameManager.instance.dex.cardDex[21].isDiscovered)){
            AudioManager.instance.Play("Item_Interact");
            GameManager.instance.holding = "Water";
            GameManager.instance.holdingImage.sprite = this.gameObject.GetComponent<Image>().sprite;
            GameManager.instance.InventoryUI.SetActive(true);
            shadowStrikeDialogue.DB = shadowWater;

            Reward_Water[] foundSources = FindObjectsOfType<Reward_Water>();
            foreach (Reward_Water _r in foundSources){
                _r.DB = haveWater;
            }

        }  
        yield return new WaitForEndOfFrame();
    }
}
