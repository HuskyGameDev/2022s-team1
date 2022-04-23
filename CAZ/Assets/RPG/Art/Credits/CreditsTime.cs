using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsTime : MonoBehaviour
{
    int time = 138;
    float count = 0;

    private void Update()
    {
        count += Time.deltaTime;
        if (count > time) {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("Menu");
        }
    }
}
