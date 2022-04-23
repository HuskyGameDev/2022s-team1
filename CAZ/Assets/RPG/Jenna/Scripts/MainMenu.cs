using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void Start(){
        
        if (!(SaveSystem.SaveGameExists())){
            GameObject.Find("Continue Button").GetComponent<Button>().interactable = false;
        }

    }

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

    public void PromptNewGameOpen(){
        if (!(SaveSystem.SaveGameExists())){
            NewGame();
        }
    }

    public void NewGame(){
        //Add a prompt here
        SaveSystem.RemoveGameData(); //Remove the game's data before going to the village. IMPORTANT.

        GameObject _gameManager = GameObject.Find("GameManager");
        if (_gameManager != null){
            GameManager gm = _gameManager.GetComponent<GameManager>();
            gm.currentLevel = GameManager.Level.VILLAGE;
            gm.deckMax = 10;
            gm.battleHp = 15;
            gm.discovered_forest = false;
            gm.discovered_cave = false;
            gm.discovered_castle = false;

            Transform ForestStartTransform = gm.startPositions[0].transform;
            gm.player.position = new Vector3(ForestStartTransform.position.x, ForestStartTransform.position.y, 0);

            for (int i = 0; i < gm.dex.cardDex.Count; i++){
                gm.dex.cardDex[i].isDiscovered = false;
            }

        }

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