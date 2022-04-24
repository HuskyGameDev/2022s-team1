using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger_OnTouchCondition : Interactable
{
    public DialogueBase DB;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerController>().speed = 0;
            other.gameObject.GetComponent<PlayerController>().canMove = false; //If I could do this another way, I would.
                                                                        //But since there is no way to stop a player from just walking past the dialouge, I forced them to stay.
                                                                        //Please ensure to set player canmove back to true in checkcondition.
            DialogueManager.instance.EnqueueDialogue(DB);
            StartCoroutine(WaitForDialogueFinish());
        }
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
