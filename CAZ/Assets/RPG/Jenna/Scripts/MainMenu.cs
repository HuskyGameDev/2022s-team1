using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame(){
        Debug.Log("Play Game");
        SceneManager.LoadScene("Village");
    }

    public void QuitGame(){
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Credits() {
        SceneManager.LoadScene("Credits");
    }

}