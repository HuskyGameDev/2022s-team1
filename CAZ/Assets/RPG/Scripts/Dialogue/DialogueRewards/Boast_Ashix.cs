using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boast_Ashix : DialogueTrigger_Condition
{

    public DialogueBase kidShowOffAshix;

    private void Start()
    {
        if (GameManager.instance.dex.cardDex[12].isDiscovered) {
            DB = kidShowOffAshix;
        }
    }

    public override IEnumerator CheckCondition()
    {
        if (GameManager.instance.dex.cardDex[12].isDiscovered)
        {
            Debug.Log("Sexy.");
            yield return null;
            //Maybe a reward later
        }
    }
}