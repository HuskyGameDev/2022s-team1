using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTrigger_Ashix : DialogueTrigger_OnTouchCondition
{

    public override IEnumerator CheckCondition()
    {
        Debug.Log("Started Ashix Battle");
        yield return new WaitForEndOfFrame();
        LoadBossEncounter();
    }

    private void LoadBossEncounter()
    {
        GameManager.instance.player.GetComponent<PlayerController>().speed = 250;
        GameManager.instance.player.GetComponent<PlayerController>().canMove = true; //I would unforce them eventually though.
        GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
        foreach (Transform child in GameManager.instance.transform)
        {
            child.gameObject.SetActive(false);
        }
        GameManager.instance.bossBattle = true;
        SceneManager.LoadScene("AIDevelopment");
    }
}
