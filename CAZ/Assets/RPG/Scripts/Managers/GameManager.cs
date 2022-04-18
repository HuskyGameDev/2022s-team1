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
    public bool discovered_forest;
    public bool discovered_cave;
    public bool discovered_castle;
    public bool bossBattle;
    public int deckMax = 10;
    public int battleHp = 15;
    public List<GameObject> respawnPositions;
    public List<GameObject> startPositions;
    public Button DeckbuilderButton;
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

    public IEnumerator DiscoverCard(string cardName)
    {
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
}
