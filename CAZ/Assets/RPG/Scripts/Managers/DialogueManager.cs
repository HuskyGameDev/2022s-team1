using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    public GameObject dialogueBox;

    public Text dialogueName;
    public Text dialogueText;
    public float initialDelay = 0.001f;

    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();

    private bool inDialogue;
    private bool isTyping;
    private float delay;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            inDialogue = false;
            isTyping = false;
            delay = initialDelay;
        }
        else
        {
            Debug.LogWarning("Dialogue Manager Instance Already Set");
        }
    }



    public void EnqueueDialogue(DialogueBase db)
    {
        // If already in dialogue, don't enqueue more
        if (inDialogue == true) return;

        inDialogue = true;
        dialogueBox.SetActive(true);
        dialogueInfo.Clear();
        

        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();

        dialogueName.text = info.name;
        dialogueText.text = info.text;

        isTyping = true;
        delay = initialDelay;
        StartCoroutine(TypeText(info));
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        dialogueText.text = "";
        foreach (char c in info.text.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;
            yield return null;
        }
        isTyping = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inDialogue)
            {
                if (isTyping)
                {
                    delay = 0;
                }
                else
                {
                    DequeueDialogue();
                }                
            }
        }
    }

    public void EndOfDialogue()
    {
        dialogueBox.SetActive(false);
        inDialogue = false;
    }
}
