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
    public Image VillageBackground;
    public Image ForestBackground;
    public Image CaveBackground;
    public Image CastleExtBackground;
    public Image CastleIntBackground;

    //add location enum
    public string guideName = "Your guide";

    private float fixedDeltaTime; // copy fixed delta time to restore when battle ends

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        fixedDeltaTime = Time.fixedDeltaTime;

        SelectEnemyType();
        SelectEnemyDeck();
        SetPlayerDeck();
        SceneSetUp();

        if (player.deck.Count < 3) {
            PlayerLose();
        }

        state = BattleState.START;

        playerHPText.text = player.health.ToString();
        enemyHPText.text = enemy.health.ToString();

        playerDeckText.text = player.deck.Count.ToString();
        enemyDeckText.text = enemy.deck.Count.ToString();

        // set guide name

        //PlayerWin();
        StartCoroutine(StartBattle());
    }

    private void Update()
    {
        if (!(state == BattleState.WON || state == BattleState.LOST)){
            if (Input.GetKey(KeyCode.Z))
            {
                Time.timeScale = 5.0f;
            }
            else {
                Time.timeScale = 1.0f;
            }
        } else {
            Time.timeScale = 1.0f;
        }
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
                    Debug.Log("Current Level: " + gameManager.currentLevel + " deck " + deckSelect+1 + " selected");
                    break;
                case GameManager.Level.FOREST:
                    enemy.deck = new List<Card>(enemyDecks.forestDecks[deckSelect].deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " deck " + deckSelect+1 + " selected");
                    break;
                case GameManager.Level.CAVE:
                    enemy.deck = new List<Card>(enemyDecks.caveDecks[deckSelect].deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " deck " + deckSelect+1 + " selected");
                    break;
                case GameManager.Level.CASTLE_EXT:
                    enemy.deck = new List<Card>(enemyDecks.castleDecks[deckSelect].deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " deck " + deckSelect+1 + " selected");
                    break;
                case GameManager.Level.CASTLE_INT:
                    enemy.deck = new List<Card>(enemyDecks.castleDecks[deckSelect].deck);
                    Debug.Log("Current Level: " + gameManager.currentLevel + " deck " + deckSelect+1 + " selected");
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
        int coinFlip = Random.Range(0, 6);
        Debug.Log("Coin Flip = " + coinFlip);
        if (coinFlip == 0)
        {
            EnemyTurn();
        }
        else {
            StartCoroutine(PlayerTurn());
        }

    }

    public void EnemyTurn() {
        // call enemy turn script
        state = BattleState.ENEMYTURN;

        // check if any cards are aggro in player field, and make them not aggro
        foreach (Card c in playerField) {
            if (c.aggro && c.type == Types.Creature) {
                c.attack -= effects.aggressionAmount;
                c.aggro = false;
                c.cardObject.GetComponent<CardDisplay>().Display(); // visual update
            }
            else if (c.aggro && c.type == Types.Boss)
            {
                c.attack -= effects.aggressionAmountBoss;
                c.aggro = false;
                c.cardObject.GetComponent<CardDisplay>().Display(); // visual update
            }
        }
        // check if any cards are shielded in enemy field, and make them not shielded
        foreach (Card c in enemyField)
        {
            if (c.shield && c.type == Types.Creature)
            {
                c.defense -= effects.shieldAmount;
                c.shield = false;
                c.cardObject.GetComponent<CardDisplay>().Display(); // visual update
            }
            else if (c.shield && c.type == Types.Boss)
            {
                c.defense -= effects.shieldAmountBoss;
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
            if (c.aggro && c.type == Types.Creature)
            {
                c.attack -= effects.aggressionAmount;
                c.aggro = false;
                c.cardObject.GetComponent<CardDisplay>().Display(); // visual update
            }
            else if (c.aggro && c.type == Types.Boss)
            {
                c.attack -= effects.aggressionAmountBoss;
                c.aggro = false;
                c.cardObject.GetComponent<CardDisplay>().Display(); // visual update
            }
        }
        // check if any cards are shielded in player field, and make them not shielded
        foreach (Card c in playerField)
        {
            if (c.shield && c.type == Types.Creature)
            {
                c.defense -= effects.shieldAmount;
                c.shield = false;
                c.cardObject.GetComponent<CardDisplay>().Display(); // visual update
            }
            else if (c.shield && c.type == Types.Boss)
            {
                c.defense -= effects.shieldAmountBoss;
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
        if (card.aggro && card.type == Types.Creature)
        {
            card.attack -= effects.aggressionAmount;
            card.aggro = false;
        }
        else if (card.aggro && card.type == Types.Boss)
        {
            card.attack -= effects.aggressionAmountBoss;
            card.aggro = false;
        }
        //check if clicked card is shielded, if so deactivate shield
        if (card.shield && card.type == Types.Creature)
        {
            card.defense -= effects.shieldAmount;
            card.shield = false;
        }
        else if (card.shield && card.type == Types.Boss)
        {
            card.defense -= effects.shieldAmountBoss;
            card.shield = false;
        }
    }

    public bool BossOnField(List<Card> field)
    {
        foreach (Card c in field)
        {
            if (c.type == Types.Boss)
            {
                return true;
            }
        }
        return false;
    }

    public void PlayerWin() {
        state = BattleState.WON;

        Time.timeScale = 1.0f;

        if (!gameManager.bossBattle) // if not boss battle
        {
            switch (gameManager.currentLevel)
            {
                case GameManager.Level.VILLAGE:
                    AudioManager.instance.Stop("Town_Battle");
                    AudioManager.instance.Play("Player_Win");
                    break;
                case GameManager.Level.FOREST:
                    AudioManager.instance.Stop("Forest_Battle");
                    AudioManager.instance.Play("Player_Win");
                    break;
                case GameManager.Level.CAVE:
                    AudioManager.instance.Stop("Cave_Battle");
                    AudioManager.instance.Play("Player_Win");
                    break;
                case GameManager.Level.CASTLE_EXT:
                    AudioManager.instance.Stop("Castle_EXT_Battle");
                    AudioManager.instance.Play("Player_Win");
                    break;
                case GameManager.Level.CASTLE_INT:
                    AudioManager.instance.Stop("Castle_INT_Battle");
                    AudioManager.instance.Play("Player_Win");
                    break;
            }
        }
        else
        { // is boss battle
            switch (gameManager.currentLevel)
            {
                case GameManager.Level.VILLAGE:
                    AudioManager.instance.Stop("Town_Boss");
                    AudioManager.instance.Play("Player_Win");
                    break;
                case GameManager.Level.FOREST:
                    AudioManager.instance.Stop("Forest_Boss");
                    AudioManager.instance.Play("Player_Win");
                    break;
                case GameManager.Level.CAVE:
                    AudioManager.instance.Stop("Cave_Boss");
                    AudioManager.instance.Play("Player_Win");
                    break;
                case GameManager.Level.CASTLE_EXT:
                    AudioManager.instance.Stop("Castle_EXT_Boss");
                    AudioManager.instance.Play("Player_Win");
                    break;
                case GameManager.Level.CASTLE_INT:
                    AudioManager.instance.Stop("Castle_INT_Boss");
                    AudioManager.instance.Play("Player_Win");
                    break;
            }
        }

        winView.SetActive(true);
    }

    public void PlayerWinButton() {
        // load scene where player left off
        Debug.Log("Win Button Pressed");
        Cursor.visible = true;

        if (!gameManager.bossBattle) // if not boss battle, load current level
        {
            switch (gameManager.currentLevel)
            {
                case GameManager.Level.VILLAGE:
                    AudioManager.instance.Stop("Player_Win");
                    AudioManager.instance.UnPause(AudioManager.instance.overworldSong);
                    SetGameManagerChildrenVisibility();
                    SceneManager.LoadScene("Village");
                    break;
                case GameManager.Level.FOREST:
                    AudioManager.instance.Stop("Player_Win");
                    AudioManager.instance.UnPause(AudioManager.instance.overworldSong);
                    SetGameManagerChildrenVisibility();
                    SceneManager.LoadScene("Forest");
                    break;
                case GameManager.Level.CAVE:
                    AudioManager.instance.Stop("Player_Win");
                    AudioManager.instance.UnPause(AudioManager.instance.overworldSong);
                    SetGameManagerChildrenVisibility();
                    SceneManager.LoadScene("Cave");
                    break;
                case GameManager.Level.CASTLE_EXT:
                    AudioManager.instance.Stop("Player_Win");
                    AudioManager.instance.UnPause(AudioManager.instance.overworldSong);
                    SetGameManagerChildrenVisibility();
                    SceneManager.LoadScene("CastleExterior");
                    break;
                case GameManager.Level.CASTLE_INT:
                    AudioManager.instance.Stop("Player_Win");
                    AudioManager.instance.UnPause(AudioManager.instance.overworldSong);
                    SetGameManagerChildrenVisibility();
                    SceneManager.LoadScene("CastleInterior");
                    break;
            }
        }
        else { // is boss battle, set player location, set current scene, load next level
            switch (gameManager.currentLevel)
            {
                case GameManager.Level.VILLAGE:
                    SetPlayerStartPosition(GameManager.Level.VILLAGE);

                    AudioManager.instance.Stop("Player_Win");

                    SetGameManagerChildrenVisibility();

                    GameManager.instance.currentLevel = GameManager.Level.FOREST;

                    SceneManager.LoadScene("Forest");
                    break;
                case GameManager.Level.FOREST:
                    SetPlayerStartPosition(GameManager.Level.FOREST);

                    AudioManager.instance.Stop("Player_Win");


                    SetGameManagerChildrenVisibility();

                    GameManager.instance.currentLevel = GameManager.Level.CAVE;

                    SceneManager.LoadScene("Cave");
                    break;
                case GameManager.Level.CAVE:
                    SetPlayerStartPosition(GameManager.Level.CAVE);

                    AudioManager.instance.Stop("Player_Win");


                    SetGameManagerChildrenVisibility();
                    GameManager.instance.currentLevel = GameManager.Level.CASTLE_EXT;
                    SceneManager.LoadScene("CastleExterior");
                    break;
                case GameManager.Level.CASTLE_EXT:
                    SetPlayerStartPosition(GameManager.Level.CASTLE_EXT);

                    AudioManager.instance.Stop("Player_Win");


                    SetGameManagerChildrenVisibility();
                    GameManager.instance.currentLevel = GameManager.Level.CASTLE_INT;
                    SceneManager.LoadScene("CastleInterior");
                    break;
                case GameManager.Level.CASTLE_INT:

                    AudioManager.instance.Stop("Player_Win");

                    SetPlayerStartPosition(GameManager.Level.VILLAGE); //Their quest is done, we outta send them home. :)
                    GameManager.instance.currentLevel = GameManager.Level.VILLAGE;
                    GameManager.instance.SaveGame();
                    SetGameManagerChildrenVisibility();
                    SceneManager.LoadScene("Credits");
                    break;
            }
        }

    }

    public void PlayerLose() {
        state = BattleState.LOST;

        Time.timeScale = 1.0f;

        if (!gameManager.bossBattle) // if not boss battle
        {
            switch (gameManager.currentLevel)
            {
                case GameManager.Level.VILLAGE:
                    AudioManager.instance.Stop("Town_Battle");
                    AudioManager.instance.Play("Player_Lose");
                    break;
                case GameManager.Level.FOREST:
                    AudioManager.instance.Stop("Forest_Battle");
                    AudioManager.instance.Play("Player_Lose");
                    break;
                case GameManager.Level.CAVE:
                    AudioManager.instance.Stop("Cave_Battle");
                    AudioManager.instance.Play("Player_Lose");
                    break;
                case GameManager.Level.CASTLE_EXT:
                    AudioManager.instance.Stop("Castle_EXT_Battle");
                    AudioManager.instance.Play("Player_Lose");
                    break;
                case GameManager.Level.CASTLE_INT:
                    AudioManager.instance.Stop("Castle_INT_Battle");
                    AudioManager.instance.Play("Player_Lose");
                    break;
            }
        }
        else { // is boss battle
            switch (gameManager.currentLevel)
            {
                case GameManager.Level.VILLAGE:
                    AudioManager.instance.Stop("Town_Boss");
                    AudioManager.instance.Play("Player_Lose");
                    break;
                case GameManager.Level.FOREST:
                    AudioManager.instance.Stop("Forest_Boss");
                    AudioManager.instance.Play("Player_Lose");
                    break;
                case GameManager.Level.CAVE:
                    AudioManager.instance.Stop("Cave_Boss");
                    AudioManager.instance.Play("Player_Lose");
                    break;
                case GameManager.Level.CASTLE_EXT:
                    AudioManager.instance.Stop("Castle_EXT_Boss");
                    AudioManager.instance.Play("Player_Lose");
                    break;
                case GameManager.Level.CASTLE_INT:
                    AudioManager.instance.Stop("Castle_INT_Boss");
                    AudioManager.instance.Play("Player_Lose");
                    break;
            }
        }
        loseDesc.text = "You feinted!\n" + guideName + "\nrescued you"; // change location guide depending on location
        loseView.SetActive(true);
    }

    public void PlayerLoseButton()
    {
        // load scene where player left off
        Debug.Log("Lose Button Pressed");
        Cursor.visible = true;
        AudioManager.instance.Stop("Player_Lose");
        AudioManager.instance.UnPause(AudioManager.instance.overworldSong);

        if (gameManager.bossBattle) {
            gameManager.bossBattle = false;
        }

        switch (gameManager.currentLevel)
        {
            case GameManager.Level.VILLAGE:
                Debug.Log("Setting player position...");
                SetPlayerRespawnPosition(GameManager.Level.VILLAGE);
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Village");
                break;
            case GameManager.Level.FOREST:
                SetPlayerRespawnPosition(GameManager.Level.FOREST);
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Forest");
                break;
            case GameManager.Level.CAVE:
                SetPlayerRespawnPosition(GameManager.Level.CAVE);
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("Cave");
                break;
            case GameManager.Level.CASTLE_EXT:
                SetPlayerRespawnPosition(GameManager.Level.CASTLE_EXT);
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("CastleExterior");
                break;
            case GameManager.Level.CASTLE_INT:
                SetPlayerRespawnPosition(GameManager.Level.CASTLE_INT);
                SetGameManagerChildrenVisibility();
                SceneManager.LoadScene("CastleInterior");
                break;
        }
    }

    public void SetPlayerRespawnPosition(GameManager.Level level)
    {
        switch (level)
        {
            case GameManager.Level.VILLAGE:
                Debug.Log("Current level is village");
                Transform villageRespawnTransform = gameManager.respawnPositions[0].transform;
                Debug.Log("Position Data Gathered");
                gameManager.player.position = new Vector3(villageRespawnTransform.position.x, villageRespawnTransform.position.y, 0);
                Debug.Log("Player position set");
                break;
            case GameManager.Level.FOREST:
                Transform forestRespawnTransform = gameManager.respawnPositions[1].transform;
                gameManager.player.position = new Vector3(forestRespawnTransform.position.x, forestRespawnTransform.position.y, 0);
                break;
            case GameManager.Level.CAVE:
                Transform caveRespawnTransform = gameManager.respawnPositions[2].transform;
                gameManager.player.position = new Vector3(caveRespawnTransform.position.x, caveRespawnTransform.position.y, 0);
                break;
            case GameManager.Level.CASTLE_EXT:
                Transform castleExtRespawnTransform = gameManager.respawnPositions[3].transform;
                gameManager.player.position = new Vector3(castleExtRespawnTransform.position.x, castleExtRespawnTransform.position.y, 0);
                break;
            case GameManager.Level.CASTLE_INT:
                Transform castleIntRespawnTransform = gameManager.respawnPositions[4].transform;
                gameManager.player.position = new Vector3(castleIntRespawnTransform.position.x, castleIntRespawnTransform.position.y, 0);
                break;
        }
    }

    public void SetPlayerStartPosition(GameManager.Level level)
    {
        switch (level)
        {
            case GameManager.Level.VILLAGE:
                Debug.Log("Current level is village");
                Transform ForestStartTransform = gameManager.startPositions[1].transform;
                Debug.Log("Position Data Gathered");
                gameManager.player.position = new Vector3(ForestStartTransform.position.x, ForestStartTransform.position.y, 0);
                Debug.Log("Player position set");
                break;
            case GameManager.Level.FOREST:
                Transform caveStartTransform = gameManager.startPositions[2].transform;
                gameManager.player.position = new Vector3(caveStartTransform.position.x, caveStartTransform.position.y, 0);
                break;
            case GameManager.Level.CAVE:
                Transform castleExtStartTransform = gameManager.startPositions[3].transform;
                gameManager.player.position = new Vector3(castleExtStartTransform.position.x, castleExtStartTransform.position.y, 0);
                break;
            case GameManager.Level.CASTLE_EXT:
                Transform castleIxtStartTransform = gameManager.startPositions[4].transform;
                gameManager.player.position = new Vector3(castleIxtStartTransform.position.x, castleIxtStartTransform.position.y, 0);
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

        AudioManager.instance.Pause(AudioManager.instance.overworldSong);

        if (gameManager.currentLevel == GameManager.Level.VILLAGE)
        {
            guideName = "Villager?";
            VillageBackground.gameObject.SetActive(true);
            player.SetHealth(GameManager.instance.battleHp);
            enemy.SetHealth(GameManager.instance.battleHp);

            if (!gameManager.bossBattle)
            {
                AudioManager.instance.Play("Town_Battle");
            }
            else {
                AudioManager.instance.Play("Town_Boss");
            }

        }
        else if (gameManager.currentLevel == GameManager.Level.FOREST)
        {
            guideName = "The Huntsman";
            ForestBackground.gameObject.SetActive(true);
            player.SetHealth(GameManager.instance.battleHp);
            enemy.SetHealth(GameManager.instance.battleHp);

            if (!gameManager.bossBattle)
            {
                AudioManager.instance.Play("Forest_Battle");
            }
            else
            {
                AudioManager.instance.Play("Forest_Boss");
            }
        }
        else if (gameManager.currentLevel == GameManager.Level.CAVE)
        {
            guideName = "The Scared Merchant";
            CaveBackground.gameObject.SetActive(true);
            player.SetHealth(GameManager.instance.battleHp);
            enemy.SetHealth(GameManager.instance.battleHp);

            if (!gameManager.bossBattle)
            {
                AudioManager.instance.Play("Cave_Battle");
            }
            else
            {
                AudioManager.instance.Play("Cave_Boss");
            }
        }
        else if (gameManager.currentLevel == GameManager.Level.CASTLE_EXT)
        {
            guideName = "The Undead Butler";
            CastleIntBackground.gameObject.SetActive(true);
            player.SetHealth(GameManager.instance.battleHp);
            enemy.SetHealth(GameManager.instance.battleHp);

            if (!gameManager.bossBattle)
            {
                AudioManager.instance.Play("Castle_EXT_Battle");
            }
            else
            {
                AudioManager.instance.Play("Caslte_INT_Boss");
            }
        }
        else if (gameManager.currentLevel == GameManager.Level.CASTLE_INT)
        {
            guideName = "A Lost Zoologist";
            CastleExtBackground.gameObject.SetActive(true);
            player.SetHealth(GameManager.instance.battleHp);
            enemy.SetHealth(GameManager.instance.battleHp);

            if (!gameManager.bossBattle)
            {
                AudioManager.instance.Play("Castle_INT_Battle");
            }
            else
            {
                AudioManager.instance.Play("Castle_INT_Boss");
            }
        }
    }

}
