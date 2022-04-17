using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTRUN, ENEMYTURN, WON, LOST}

public enum ActiveEffect { NONE, HEALING_POTION, SACRIFICE, SHADOW_STRIKE, SLEIGHT_OF_HAND, AGGRESSION, SHIELD, REVIVE }
public class EncounterManager : MonoBehaviour
{
    public GameManager gameManager;

    public BattleState state;
    public EnemyDecks enemyDecks;
    public int turnNum;
    public int enemyAvailableFieldSlots;
    public int playerAvailableFieldSlots;
    public ActiveEffect activeEffect;

    public List<Card> playerField;
    public List<Transform> playerFieldSlots;
    //public Transform playerFieldSlot1;
    //public Transform playerFieldSlot2;
    //public Transform playerFieldSlot3;
    public Transform playerEffectSlot;
    public Text playerHPText;
    public Text playerDeckText;

    public List<Card> enemyField;
    public List<Transform> enemyFieldSlots;
    public List<int> enemyFieldSlotAvailability;

    //public Transform enemyFieldSlot1;
    //public Transform enemyFieldSlot2;
    //public Transform enemyFieldSlot3;
    public Transform enemyEffectSlot;
    public Text handNum;
    public Text enemyHPText;
    public Text enemyDeckText;
    //public Dictionary<Transform, int> enemyFieldSlots;

    public AI_Standard enemy;
    public AI_1_Standard_Minimize AI1;
    public AI_2_Standard_Maxmimize AI2;
    public AI_3_Offensive_Minimize AI3;
    public AI_4_Offensive_Maximize AI4;
    public AI_5_Defensive_Minimize AI5;
    public AI_6_Defensive_Maximize AI6;
    public PlayerUnit player;
    public Button indicator;
    public CardEffects effects;
    public GameObject cardPrefab;
    public CursorController cursorController;
    public DiscardController enemyDiscardController;
    public DiscardController playerDiscardController;
    public CardZoom cardZoom;
    public GameObject winView;
    public GameObject loseView;
    public Text loseDesc;
    public Image background;
    public Sprite VillageSprite;
    public Sprite ForestSprite;
    public Sprite CaveSprite;
    public Sprite CastleExtSprite;
    public Sprite CastleIntSprite;

    //add location enum
    public string guideName = "Your guide";

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        SelectEnemyType();
        SelectEnemyDeck();
        SetPlayerDeck();
        SceneSetUp();

        if (player.deck.Count == 0) {
            PlayerLose();
        }

        state = BattleState.START;

        playerHPText.text = player.health.ToString();
        enemyHPText.text = enemy.health.ToString();

        playerDeckText.text = player.deck.Count.ToString();
        enemyDeckText.text = enemy.deck.Count.ToString();

        // set guide name

        StartCoroutine(StartBattle());
    }

    void SelectEnemyType() {
        Debug.Log("Selecting enemy...");
        int enemySelect = Random.Range(1, 7);
        switch (enemySelect)
        {
            case 1:
                enemy = AI1;
                Debug.Log("AI-1 Selected");
                break;
            case 2:
                enemy = AI2;
                Debug.Log("AI-2 Selected");
                break;
            case 3:
                enemy = AI3;
                Debug.Log("AI-3 Selected");
                break;
            case 4:
                enemy = AI4;
                Debug.Log("AI-4 Selected");
                break;
            case 5:
                enemy = AI5;
                Debug.Log("AI-5 Selected");
                break;
            case 6:
                enemy = AI6;
                Debug.Log("AI-6 Selected");
                break;
        }
    }

    void SelectEnemyDeck() {
        Debug.Log("Selecting deck");
        int deckSelect = Random.Range(0, 3);
        if (!gameManager.bossBattle) // if not boss battles, select random deck from list of decks based on area
        {
            switch (gameManager.currentLevel)
            {
                case GameManager.Level.VILLAGE:
                    enemy.deck = new List<Card>(enemyDecks.townDecks[deckSelect].deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " deck " + deckSelect + " selected");
                    break;
                case GameManager.Level.FOREST:
                    enemy.deck = new List<Card>(enemyDecks.forestDecks[deckSelect].deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " deck " + deckSelect + " selected");
                    break;
                case GameManager.Level.CAVE:
                    enemy.deck = new List<Card>(enemyDecks.caveDecks[deckSelect].deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " deck " + deckSelect + " selected");
                    break;
                case GameManager.Level.CASTLE_EXT:
                    enemy.deck = new List<Card>(enemyDecks.castleDecks[deckSelect].deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " deck " + deckSelect + " selected");
                    break;
                case GameManager.Level.CASTLE_INT:
                    enemy.deck = new List<Card>(enemyDecks.castleDecks[deckSelect].deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " deck " + deckSelect + " selected");
                    break;
            }
        }
        else // else, battle is boss fight, choose boss deck form list based on area
        {
            switch (gameManager.currentLevel)
            {
                case GameManager.Level.VILLAGE:
                    enemy.deck = new List<Card>(enemyDecks.townBossDeck.deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " boss deck selected");
                    break;
                case GameManager.Level.FOREST:
                    enemy.deck = new List<Card>(enemyDecks.forestBossDeck.deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " boss deck selected");
                    break;
                case GameManager.Level.CAVE:
                    enemy.deck = new List<Card>(enemyDecks.caveBossDeck.deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " boss deck selected");
                    break;
                case GameManager.Level.CASTLE_EXT:
                    enemy.deck = new List<Card>(enemyDecks.castleBossDeck.deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " boss deck selected");
                    break;
                case GameManager.Level.CASTLE_INT:
                    enemy.deck = new List<Card>(enemyDecks.castleBossDeck.deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " boss deck selected");
                    break;
            }
        }
    }

    void SetPlayerDeck() {
        player.deck = new List<Card>(gameManager.GetComponent<Deck>().deck);
    }

    /*private void Update()
    {
        
    }*/

    IEnumerator StartBattle() {
        //Shuffle Decks
        //Flip Coin for first turn
        //Draw Cards for both player and enemy

        Debug.Log("Starting Battle");

        Debug.Log("Shuffling Decks");
        int playerShuffleNum = Random.Range(10, 100);
        int enemyShuffleNum = Random.Range(10, 100);
        Debug.Log("Player Shuffle Seed = " + playerShuffleNum);
        Debug.Log("Enemy Shuffle Seed = " + enemyShuffleNum);
        player.deck.Shuffle(playerShuffleNum);
        enemy.deck.Shuffle(enemyShuffleNum);

        yield return new WaitForSeconds(2f);

        //Determine who goes first
        StartCoroutine(PlayerTurn());
        //EnemyTurn();
    }

    public void EnemyTurn() {
        // call enemy turn script
        state = BattleState.ENEMYTURN;

        // check if any cards are aggro in player field, and make them not aggro
        foreach (Card c in playerField) {
            if (c.aggro) {
                c.attack -= effects.aggressionAmount;
                c.aggro = false;
                c.cardObject.GetComponent<CardDisplay>().Display(); // visual update
            }
        }
        // check if any cards are shielded in enemy field, and make them not shielded
        foreach (Card c in enemyField)
        {
            if (c.shield)
            {
                c.defense -= effects.shieldAmount;
                c.shield = false;
                c.cardObject.GetComponent<CardDisplay>().Display(); // visual update
            }
        }

        indicator.GetComponentInChildren<Text>().text = "Enemy Turn";
        StartCoroutine(enemy.PlayTurn());
    }

    public IEnumerator PlayerTurn()
    {
        // call enemy turn script
        state = BattleState.PLAYERTRUN;

        // check if any cards are aggro in enemy field, and make them not aggro
        foreach (Card c in enemyField)
        {
            if (c.aggro)
            {
                c.attack -= effects.aggressionAmount;
                c.aggro = false;
                c.cardObject.GetComponent<CardDisplay>().Display(); // visual update
            }
        }
        // check if any cards are shielded in player field, and make them not shielded
        foreach (Card c in playerField)
        {
            if (c.shield)
            {
                c.defense -= effects.shieldAmount;
                c.shield = false;
                c.cardObject.GetComponent<CardDisplay>().Display(); // visual update
            }
        }

        indicator.GetComponentInChildren<Text>().text = "Your Turn";

        foreach (Card c in playerField) {
            c.turnAction = TurnAction.NotUsed;
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(player.PlayTurn());
    }

    public void RemoveLingeringEffects(Card card)
    {
        //check if clicked card is aggro, if so deactivate aggro
        if (card.aggro)
        {
            card.attack -= effects.aggressionAmount;
            card.aggro = false;
        }
        //check if clicked card is shielded, if so deactivate shield
        if (card.shield)
        {
            card.defense -= effects.shieldAmount;
            card.shield = false;
        }
    }

    public void PlayerWin() {
        state = BattleState.WON;
        winView.SetActive(true);
    }

    public void PlayerWinButton() {
        // load scene where player left off
        Debug.Log("Win Button Pressed");


        switch (gameManager.currentLevel)
        {
            case GameManager.Level.VILLAGE:
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Village");
                break;
            case GameManager.Level.FOREST:
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Forest");
                break;
            case GameManager.Level.CAVE:
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Cave");
                break;
            case GameManager.Level.CASTLE_EXT:
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Castle Exterior");
                break;
            case GameManager.Level.CASTLE_INT:
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Castle Interior");
                break;
        }

    }

    public void PlayerLose() {
        state = BattleState.LOST;
        loseDesc.text = "You feinted!\n" + guideName + "\nrescued you"; // change location guide depending on location
        loseView.SetActive(true);
    }

    public void PlayerLoseButton()
    {
        // load scene where player left off
        Debug.Log("Lose Button Pressed");

        switch (gameManager.currentLevel)
        {
            case GameManager.Level.VILLAGE:
                Debug.Log("Setting player position...");
                SetPlayerPosition(GameManager.Level.VILLAGE);
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Village");
                break;
            case GameManager.Level.FOREST:
                SetPlayerPosition(GameManager.Level.FOREST);
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Forest");
                break;
            case GameManager.Level.CAVE:
                SetPlayerPosition(GameManager.Level.CAVE);
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Cave");
                break;
            case GameManager.Level.CASTLE_EXT:
                SetPlayerPosition(GameManager.Level.CASTLE_EXT);
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Castle Exterior");
                break;
            case GameManager.Level.CASTLE_INT:
                SetPlayerPosition(GameManager.Level.CASTLE_INT);
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Castle Interior");
                break;
        }
    }

    public void SetPlayerPosition(GameManager.Level level)
    {
        switch (level)
        {
            case GameManager.Level.VILLAGE:
                Debug.Log("Current level is village");
                Transform villageRespawnTransform = gameManager.respawnPositions[0].transform;
                Debug.Log("Position Data Gathered");
                gameManager.player.position = new Vector3(villageRespawnTransform.position.x, villageRespawnTransform.position.y, villageRespawnTransform.position.z);
                Debug.Log("Player position set");
                break;
            case GameManager.Level.FOREST:
                Transform forestRespawnTransform = gameManager.respawnPositions[1].transform;
                gameManager.player.position = new Vector3(forestRespawnTransform.position.x, forestRespawnTransform.position.y, forestRespawnTransform.position.z);
                break;
            case GameManager.Level.CAVE:
                Transform caveRespawnTransform = gameManager.respawnPositions[2].transform;
                gameManager.player.position = new Vector3(caveRespawnTransform.position.x, caveRespawnTransform.position.y, caveRespawnTransform.position.z);
                break;
            case GameManager.Level.CASTLE_EXT:
                Transform castleExtRespawnTransform = gameManager.respawnPositions[3].transform;
                gameManager.player.position = new Vector3(castleExtRespawnTransform.position.x, castleExtRespawnTransform.position.y, castleExtRespawnTransform.position.z);
                break;
            case GameManager.Level.CASTLE_INT:
                Transform castleIntRespawnTransform = gameManager.respawnPositions[4].transform;
                gameManager.player.position = new Vector3(castleIntRespawnTransform.position.x, castleIntRespawnTransform.position.y, castleIntRespawnTransform.position.z);
                break;
        }
    }

    public void SetGameManagerChildrenVisibility() {
        foreach (Transform child in GameManager.instance.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void SceneSetUp() {
        if (gameManager.currentLevel == GameManager.Level.VILLAGE) {
            guideName = "Villager?";
        }
        else if (gameManager.currentLevel == GameManager.Level.FOREST)
        {
            guideName = "The Huntsman";
        }
        else if (gameManager.currentLevel == GameManager.Level.CAVE)
        {
            guideName = "The Scared Merchant";
        }
        else if (gameManager.currentLevel == GameManager.Level.CASTLE_INT)
        {
            guideName = "The Undead Butler";
        }
        else if (gameManager.currentLevel == GameManager.Level.CASTLE_EXT)
        {
            guideName = "A Lost Zoologist";
        }
    }

}
