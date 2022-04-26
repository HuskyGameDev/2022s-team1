using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTrigger_Zekeke : DialogueTrigger_OnTouchCondition
{
    private void Start()
    {
        if (GameManager.instance.discovered_forest)
        {
            canInteract = false;
        }
    }

    public override IEnumerator CheckCondition()
    {
        Debug.Log("Started Zekeke Battle");
        yield return new WaitForEndOfFrame();
        LoadBossEncounter();
    }

    private void LoadBossEncounter()
    {
        GameManager.instance.player.GetComponent<PlayerController>().speed = 250;
        GameManager.instance.player.GetComponent<PlayerController>().canMove = true; //I would unforce them eventually though.
        GameManager.instance.player.GetComponent<PlayerController>().interactIcon.SetActive(false);
        canInteract = false;
        foreach (Transform child in GameManager.instance.transform)
        {
            child.gameObject.SetActive(false);
        }
        GameManager.instance.bossBattle = true;
        SceneManager.LoadScene("AIDevelopment");
    }
}
