using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            GameManager.instance.bossBattle = true;
            LoadBossEncounter();
        }
    }


    private void LoadBossEncounter()
    {
        foreach (Transform child in GameManager.instance.transform)
        {
            child.gameObject.SetActive(false);
        }
        SceneManager.LoadScene("AIDevelopment");
    }
}
