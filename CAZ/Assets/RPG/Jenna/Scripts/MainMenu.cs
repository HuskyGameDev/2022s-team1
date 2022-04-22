using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame(){
        Debug.Log("Play Game");

        //We get the game object of the game manager, determine if it exists.
        //And then based on the level we're supposed to be on, load said level.

        GameObject _gameObj = GameObject.Find("GameManager");

        if (!(_gameObj == null)){

            GameManager gm = _gameObj.GetComponent<GameManager>();

            switch (gm.currentLevel){
                case GameManager.Level.FOREST:
                    SceneManager.LoadScene("Forest");
                    break;
                case GameManager.Level.CAVE:
                    SceneManager.LoadScene("Cave");
                    break;
                case GameManager.Level.CASTLE_EXT:
                    SceneManager.LoadScene("CastleExterior");
                    break;
                case GameManager.Level.CASTLE_INT:
                    SceneManager.LoadScene("CastleInterior");
                    break;
                default:        
                    SceneManager.LoadScene("Village");
                    break;
            }

        } else {
            SceneManager.LoadScene("Village");
        }

    }

    public void LoadGame(){
        GameManager.instance.LoadGame();
    }

    public void QuitGame(){
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Credits() {
        SceneManager.LoadScene("Credits");
    }

}