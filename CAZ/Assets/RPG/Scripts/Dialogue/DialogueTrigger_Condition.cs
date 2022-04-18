using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public virtual IEnumerator CheckCondition() {
        yield return new WaitForEndOfFrame();
        /*
        if (GameManager.instance.holding == "Carrot")
        {
            Debug.Log("Completed Carrot Dioalogue");
            GameManager.instance.InventoryUI.SetActive(false);
            GameManager.instance.holding = null;
            StartCoroutine(GameManager.instance.DiscoverCard("Aggression"));
        }
        else if (GameManager.instance.holding == "DogTreat") {
            Debug.Log("Completed Dodogger Dioalogue");
            GameManager.instance.InventoryUI.SetActive(false);
            GameManager.instance.holding = null;
            StartCoroutine(GameManager.instance.DiscoverCard("Dodogger"));
        }

        if (this.gameObject.tag == "DogTreat") {
            AudioManager.instance.Play("Item_Interact");
            GameManager.instance.holding = "DogTreat";
            GameManager.instance.holdingImage.sprite = this.gameObject.GetComponent<Image>().sprite;
            GameManager.instance.InventoryUI.SetActive(true);
        }
        */
    }
}
