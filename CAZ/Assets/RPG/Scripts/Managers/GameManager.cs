using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public enum Level { VILLAGE, FOREST, CAVE, CASTLE_EXT, CASTLE_INT }

    public static GameManager instance;
    public CardDex dex;
    public Deck deck;
    public GameObject InventoryUI;
    public Image holdingImage;
    public Image LoadBlackness;
    public Transform player;
    public string holding;
    public Level currentLevel = Level.VILLAGE;
    public bool discovered_forest;
    public bool discovered_cave;
    public bool discovered_castle;
    public bool bossBattle;
    public int deckMax = 10;
    public int battleHp = 15;
    public List<GameObject> respawnPositions;
    public List<GameObject> startPositions;
    public List<GameObject> endPositions;
    public Button DeckbuilderButton;
    public GameObject discoveredUI;
    public Text discoveredText;
    public GameObject discoveredCreature;
    public GameObject discoveredBoss;
    public GameObject discoveredEffect;
    public GameObject pauseMenu;

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

        //Load the game everytime we enter in.

    }

    private void Start()
    {
        //AudioManager.instance.Play("Main_Theme");
        Debug.Log("Playing theme");
        //AudioManager.instance.Play("Main_Theme");
        //AudioManager.instance.overworldSong = "Main_Theme";
        GameManager.instance.doLoad();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenu.activeInHierarchy)
                pauseMenu.SetActive(false);
            else if (!pauseMenu.activeInHierarchy)
                pauseMenu.SetActive(true);
        }
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioManager.instance.Stop(AudioManager.instance.overworldSong);
                SceneManager.LoadScene("Credits");
            }
            /*if (Input.GetKeyDown(KeyCode.G))
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
                SceneManager.LoadScene("Puzzle");
            }*/
        }

    public void loadDeckBuilder() {
        AudioManager.instance.Play("NPC_Interact");
        AudioManager.instance.Pause(AudioManager.instance.overworldSong);
        SceneManager.LoadScene("DeckBuilder");
    }

    public IEnumerator DiscoverCard(string cardName)
    {
        GameManager.instance.DeckbuilderButton.interactable = false;
        CardDex.CardEntry entry = GameManager.instance.GetComponent<CardDex>().cardDex.Find((x) => x.card.name == cardName);
        entry.isDiscovered = true;

        GameManager.instance.discoveredText.text = "Discovered: " + cardName + "!";
        GameObject discoveredCardUI = selectDiscoveredImage(entry);
        discoveredCardUI.GetComponent<CardDisplay>().card = entry.card;
        discoveredCardUI.GetComponent<CardDisplay>().Display();


        AudioManager.instance.Play("Ding");
        discoveredCardUI.SetActive(true);
        GameManager.instance.discoveredUI.SetActive(true);

        yield return new WaitForSeconds(3f);

        discoveredCardUI.SetActive(false);
        GameManager.instance.discoveredUI.SetActive(false);
        GameManager.instance.DeckbuilderButton.interactable = true;
    }

    public GameObject selectDiscoveredImage(CardDex.CardEntry entry)
    {
        if (entry.card.type == Types.Creature)
        {
            return GameManager.instance.discoveredCreature;
        }
        else if (entry.card.type == Types.Boss)
        {
            return GameManager.instance.discoveredBoss;
        }
        else if (entry.card.type == Types.Effect)
        {
            return GameManager.instance.discoveredEffect;
        }
        return GameManager.instance.discoveredCreature;
    }

    
    public void SaveGame()
    {
        SaveSystem.SaveGameData(GameManager.instance);
        pauseMenu.SetActive(false);
    }

    public void LoadGame()
    {
        SaveGameData data = SaveSystem.LoadGameData();

        data.dex.GetDiscoveredCards(GameManager.instance.dex);
        GameManager.instance.currentLevel = data.level;
        GameManager.instance.deckMax = data.deckMax;
        GameManager.instance.battleHp = data.battleHP;
        GameManager.instance.discovered_forest = data.discovered_forest;
        GameManager.instance.discovered_cave = data.discovered_cave;
        GameManager.instance.discovered_castle = data.discovered_castle;

        //We actually want to ensure the player is at the latest stage of the game.
        //If you use else-ifs, it's gonna start them at the forest.
        //Also copying your code for player transform starts Sawyer, hope that's cool
        if (GameManager.instance.currentLevel == Level.FOREST) {
            Transform ForestStartTransform = startPositions[1].transform;
            player.position = new Vector3(ForestStartTransform.position.x, ForestStartTransform.position.y, 0);
            SceneManager.LoadScene("Forest");
        }
        
        else if (GameManager.instance.currentLevel == Level.CAVE) {
            Transform caveStartTransform = startPositions[2].transform;
            player.position = new Vector3(caveStartTransform.position.x, caveStartTransform.position.y, 0);
            SceneManager.LoadScene("Cave");
        }
        
        else if (GameManager.instance.currentLevel == Level.CASTLE_EXT)
        {
            Transform castleExtStartTransform = startPositions[3].transform;
            player.position = new Vector3(castleExtStartTransform.position.x, castleExtStartTransform.position.y, 0);
            SceneManager.LoadScene("CastleExterior");
        }

        else if (GameManager.instance.currentLevel == Level.CASTLE_INT)
        {
            Transform castleExtStartTransform = startPositions[4].transform;
            player.position = new Vector3(castleExtStartTransform.position.x, castleExtStartTransform.position.y, 0);
            SceneManager.LoadScene("CastleInterior");
        }
    }

    public void doLoad(){
        
        string path = Application.persistentDataPath + "/savefile.caz";
        if (File.Exists(path)) {        
            this.LoadGame();
        }

    }
    

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void MainMenu()
    {
        Debug.Log("Returning to main menu");
        AudioManager.instance.Stop(AudioManager.instance.overworldSong);
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
    }
}
