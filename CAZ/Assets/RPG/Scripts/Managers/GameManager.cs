using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Level { VILLAGE, FOREST, CAVE, CASTLE_EXT, CASTLE_INT }

    public static GameManager instance;
    public GameObject InventoryUI;
    public Image holdingImage;
    public Transform player;
    public string holding;
    public Level currentLevel = Level.VILLAGE;
    public bool bossBattle;
    public int deckMax;
    public List<GameObject> respawnPositions;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }
            SceneManager.LoadScene("DeckBuilder");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            SceneManager.LoadScene("AIDevelopment");
        }
    }
}
