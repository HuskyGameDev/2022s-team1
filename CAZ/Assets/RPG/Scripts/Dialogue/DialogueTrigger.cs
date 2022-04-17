using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public DialogueBase DB;

    public override void Interact()
    {
        AudioManager.instance.Play("NPC_Interact");
        DialogueManager.instance.EnqueueDialogue(DB);
    }
}
