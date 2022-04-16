using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_Condition : Interactable
{
    public DialogueBase DB;

    public override void Interact()
    {
        DialogueManager.instance.EnqueueDialogue(DB);

        StartCoroutine(WaitForDialogueFinish());
    }

    IEnumerator WaitForDialogueFinish() {
        yield return new WaitUntil(() => !DialogueManager.instance.inDialogue);
        CheckCondition();
    }

    public void CheckCondition() {
        if (GameManager.instance.holding == "Carrot") {
            Debug.Log("Completed Carrot Dioalogue");

            CardDex.CardEntry entry = GameManager.instance.GetComponent<CardDex>().cardDex.Find((x) => x.card.name == "Aggression");
            entry.isDiscovered = true;

            GameManager.instance.InventoryUI.SetActive(false);
        }
    }
}
