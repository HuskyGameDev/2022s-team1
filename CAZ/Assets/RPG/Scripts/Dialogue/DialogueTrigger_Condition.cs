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
        StartCoroutine(CheckCondition());
    }

    public IEnumerator CheckCondition() {
        if (GameManager.instance.holding == "Carrot") {
            Debug.Log("Completed Carrot Dioalogue");

            CardDex.CardEntry entry = GameManager.instance.GetComponent<CardDex>().cardDex.Find((x) => x.card.name == "Aggression");
            entry.isDiscovered = true;

            GameManager.instance.discoveredText.text = "Discovered: Aggression!";
            GameObject discoveredCardUI = selectDiscoveredImage(entry);

            discoveredCardUI.SetActive(true);
            GameManager.instance.discoveredUI.SetActive(true);
            GameManager.instance.InventoryUI.SetActive(false);
            GameManager.instance.holding = null;

            yield return new WaitForSeconds(3f);

            discoveredCardUI.SetActive(false);
            GameManager.instance.discoveredUI.SetActive(false);
        }
    }

    public GameObject selectDiscoveredImage(CardDex.CardEntry entry) {
        if (entry.card.type == Types.Creature)
        {
            return GameManager.instance.discoveredCreature;
        }
        else if (entry.card.type == Types.Boss)
        {
            return GameManager.instance.discoveredBoss;
        }
        else if (entry.card.type == Types.Effect)
        {
            return GameManager.instance.discoveredEffect;
        }
        return GameManager.instance.discoveredCreature;
    }
}
