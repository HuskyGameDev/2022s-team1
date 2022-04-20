using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dev_LocationManager : MonoBehaviour
{
    public bool forest;
    public bool cave;
    public bool castle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.instance.Stop(AudioManager.instance.overworldSong);

        if (other.gameObject.tag == "Player")
        {
            if (forest) {
                Transform ForestStartTransform = GameManager.instance.startPositions[1].transform;
                GameManager.instance.player.position = new Vector3(ForestStartTransform.position.x, ForestStartTransform.position.y, 0);
                GameManager.instance.currentLevel = GameManager.Level.FOREST;
                SceneManager.LoadScene("Forest");
            }
            else if (cave)
            {
                Transform caveStartTransform = GameManager.instance.startPositions[2].transform;
                GameManager.instance.player.position = new Vector3(caveStartTransform.position.x, caveStartTransform.position.y, 0);
                GameManager.instance.currentLevel = GameManager.Level.CAVE;
                SceneManager.LoadScene("Cave");
            }
            else if (castle) {
                Transform castleExtStartTransform = GameManager.instance.startPositions[3].transform;
                GameManager.instance.player.position = new Vector3(castleExtStartTransform.position.x, castleExtStartTransform.position.y, 0);
                GameManager.instance.currentLevel = GameManager.Level.CASTLE_EXT;
                SceneManager.LoadScene("CastleExterior");
            }
        }
    }
}
