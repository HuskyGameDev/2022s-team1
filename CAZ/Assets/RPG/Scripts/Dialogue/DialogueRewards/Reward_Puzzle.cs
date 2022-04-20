using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reward_Puzzle : DialogueTrigger_Condition
{
    public Reward_Hoasteban hoasteban_PuzzleDialogue;
    public DialogueBase PuzzleCompleteDB;
    public DialogueBase hoastebanPuzzleCompleteDB;

    private void Start()
    {
        if (GameManager.instance.holding == "Puzzle") {
            DB = PuzzleCompleteDB;
        }
    }

    public override IEnumerator CheckCondition()
    {

        if (GameManager.instance.holding != "Puzzle") {
            GameManager.instance.holding = "PuzzlePrime";
        }

        if (GameManager.instance.holding == "PuzzlePrime")
        {
            AudioManager.instance.Play("Item_Interact");
            GameManager.instance.holdingImage.sprite = this.gameObject.GetComponent<Image>().sprite;
            GameManager.instance.InventoryUI.SetActive(true);
            foreach (Transform child in GameManager.instance.transform)
            {
                child.gameObject.SetActive(false);
            }
            GameManager.instance.holding = "Puzzle";
            SceneManager.LoadScene("Puzzle");
        }
        else if (GameManager.instance.holding == "Puzzle") {
            hoasteban_PuzzleDialogue.DB = hoastebanPuzzleCompleteDB;
        }


        yield return new WaitForEndOfFrame();
    }
}
