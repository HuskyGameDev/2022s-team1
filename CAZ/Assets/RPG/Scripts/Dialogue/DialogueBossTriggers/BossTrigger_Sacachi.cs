using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTrigger_Sacachi : DialogueTrigger_Condition
{
    public override IEnumerator CheckCondition()
    {
        Debug.Log("Started Sacachi Battle");
        yield return new WaitForEndOfFrame();
        LoadBossEncounter();
    }

    private void LoadBossEncounter()
    {
        GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
        foreach (Transform child in GameManager.instance.transform)
        {
            child.gameObject.SetActive(false);
        }
        GameManager.instance.bossBattle = true;
        SceneManager.LoadScene("AIDevelopment");
    }
}
