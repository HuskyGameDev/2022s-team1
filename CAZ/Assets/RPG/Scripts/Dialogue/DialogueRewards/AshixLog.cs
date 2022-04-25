using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshixLog : DialogueTrigger_Condition
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.instance.discovered_castle){
            this.gameObject.SetActive(false);
        }
    }

    public override IEnumerator CheckCondition()
    {
        yield return null;
    }
}
