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
    public List<GameObject> startPositions;
    public GameObject discoveredUI;
    public Text discoveredText;
    public GameObject discoveredCreature;
    public GameObject discoveredBoss;
    public GameObject discoveredEffect;

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

    private void Start()
    {
        //AudioManager.instance.Play("Main_Theme");
        Debug.Log("Playing theme");
        AudioManager.instance.Play("Main_Theme");
        AudioManager.instance.overworldSong = "Main_Theme";
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            SceneManager.LoadScene("AIDevelopment");
        }
    }

    public void loadDeckBuilder() {
        AudioManager.instance.Play("NPC_Interact");
        AudioManager.instance.Pause(AudioManager.instance.overworldSong);
        SceneManager.LoadScene("DeckBuilder");
    }
}
