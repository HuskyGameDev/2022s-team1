using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Dialogue Manager Instance Already Set");
        }
    }

    public GameObject dialogueBox;

    public Text dialogueName;
    public Text dialogueText;
    public float delay = 0.001f;

    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();

    //private bool buffer;
    private bool inDialogue;

    public void EnqueueDialogue(DialogueBase db)
    {
        //buffer = true;
        dialogueBox.SetActive(true);
        dialogueInfo.Clear();
        inDialogue = true;
        //StartCoroutine(BufferTimer());

        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
       // if (buffer == true) return;
        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();

        dialogueName.text = info.name;
        dialogueText.text = info.text;

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
    }

    /*IEnumerator BufferTimer()
    {
        yield return new WaitForSeconds(0.1f);
        buffer = false;
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inDialogue)
            {
                DequeueDialogue();
            }
        }
    }

    public void EndOfDialogue()
    {
        Debug.Log("EndOfDialogue");
        dialogueBox.SetActive(false);
        inDialogue = false;
    }
}
