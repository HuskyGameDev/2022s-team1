using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldController: MonoBehaviour
{

    public bool forwardTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SetPlayerPosition(GameManager.instance.currentLevel, forwardTrigger);
            SetGameManagerChildrenVisibility();
        }
    }

    public void SetPlayerPosition(GameManager.Level level, bool forwardTrigger)
    {
        switch (level)
        {
            case GameManager.Level.VILLAGE:
                if (forwardTrigger)
                {
                    Debug.Log("Current level is village");
                    Transform ForestStartTransform = GameManager.instance.startPositions[1].transform;
                    GameManager.instance.player.position = new Vector3(ForestStartTransform.position.x, ForestStartTransform.position.y, 0);
                    Debug.Log("Player position set");
                    GameManager.instance.currentLevel = GameManager.Level.FOREST;
                    AudioManager.instance.Stop(AudioManager.instance.overworldSong);
                    SceneManager.LoadScene("Forest");
                }
                else if (!forwardTrigger) {
                    //Cannot go backward from village
                }
                break;
            case GameManager.Level.FOREST:
                if (forwardTrigger)
                {
                    Transform caveStartTransform = GameManager.instance.startPositions[2].transform;
                    GameManager.instance.player.position = new Vector3(caveStartTransform.position.x, caveStartTransform.position.y, 0);
                    GameManager.instance.currentLevel = GameManager.Level.CAVE;
                    AudioManager.instance.Stop(AudioManager.instance.overworldSong);
                    SceneManager.LoadScene("Cave");
                }
                else if (!forwardTrigger)
                {
                    Transform caveStartTransform = GameManager.instance.endPositions[0].transform;
                    GameManager.instance.player.position = new Vector3(caveStartTransform.position.x, caveStartTransform.position.y, 0);
                    GameManager.instance.currentLevel = GameManager.Level.VILLAGE;
                    AudioManager.instance.Stop(AudioManager.instance.overworldSong);
                    SceneManager.LoadScene("Village");
                }
                break;
            case GameManager.Level.CAVE:
                if (forwardTrigger)
                {
                    Transform castleExtStartTransform = GameManager.instance.startPositions[3].transform;
                    GameManager.instance.player.position = new Vector3(castleExtStartTransform.position.x, castleExtStartTransform.position.y, 0);
                    GameManager.instance.currentLevel = GameManager.Level.CASTLE_EXT;
                    AudioManager.instance.Stop(AudioManager.instance.overworldSong);
                    SceneManager.LoadScene("CastleExterior");
                }
                else if (!forwardTrigger) {
                    Transform castleExtStartTransform = GameManager.instance.endPositions[1].transform;
                    GameManager.instance.player.position = new Vector3(castleExtStartTransform.position.x, castleExtStartTransform.position.y, 0);
                    GameManager.instance.currentLevel = GameManager.Level.FOREST;
                    AudioManager.instance.Stop(AudioManager.instance.overworldSong);
                    SceneManager.LoadScene("Forest");
                }
                break;
            case GameManager.Level.CASTLE_EXT:
                if (forwardTrigger)
                {
                    Transform castleIxtStartTransform = GameManager.instance.startPositions[4].transform;
                    GameManager.instance.player.position = new Vector3(castleIxtStartTransform.position.x, castleIxtStartTransform.position.y, 0);
                    GameManager.instance.currentLevel = GameManager.Level.CASTLE_INT;
                    AudioManager.instance.Stop(AudioManager.instance.overworldSong);
                    SceneManager.LoadScene("CastleInterior");
                }
                else if (!forwardTrigger)
                {
                    Transform castleIxtStartTransform = GameManager.instance.endPositions[2].transform;
                    GameManager.instance.player.position = new Vector3(castleIxtStartTransform.position.x, castleIxtStartTransform.position.y, 0);
                    GameManager.instance.currentLevel = GameManager.Level.CAVE;
                    AudioManager.instance.Stop(AudioManager.instance.overworldSong);
                    SceneManager.LoadScene("Cave");
                }
                break;
        }
    }

    public void SetGameManagerChildrenVisibility()
    {
        foreach (Transform child in GameManager.instance.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
