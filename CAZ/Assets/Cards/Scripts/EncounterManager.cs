using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTRUN, ENEMYTURN, WON, LOST}

public class EncounterManager : MonoBehaviour
{

    public BattleState state;
    public int turnNum;
    public int enemyAvailableFieldSlots;
    public int playerAvailableFieldSlots;

    public List<Card> playerField;
    public List<Transform> playerFieldSlots;
    //public Transform playerFieldSlot1;
    //public Transform playerFieldSlot2;
    //public Transform playerFieldSlot3;
    public Transform playerEffectSlot;
    public Text playerHPText;

    public List<Card> enemyField;
    public List<Transform> enemyFieldSlots;
    public List<int> enemyFieldSlotAvailability;
    //public Transform enemyFieldSlot1;
    //public Transform enemyFieldSlot2;
    //public Transform enemyFieldSlot3;
    public Transform enemyEffectSlot;
    public Text handNum;
    public Text enemyHPText;
    //public Dictionary<Transform, int> enemyFieldSlots;

    public AI_Standard enemy;
    public PlayerUnit player;
    public Button indicator;
    public CardEffects effects;
    public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        playerHPText.text = player.health.ToString();
        enemyHPText.text = enemy.health.ToString();
        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle() {
        //Shuffle Decks
        //Flip Coin for first turn
        //Draw Cards for both player and enemy

        Debug.Log("Starting Battle");

        yield return new WaitForSeconds(2f);

        //Determine who goes first
        StartCoroutine(PlayerTurn());
    }

    public void EnemyTurn() {
        // call enemy turn script
        state = BattleState.ENEMYTURN;
        indicator.GetComponentInChildren<Text>().text = "Enemy Turn";
        StartCoroutine(enemy.PlayTurn());
    }

    public IEnumerator PlayerTurn()
    {
        // call enemy turn script
        state = BattleState.PLAYERTRUN;
        indicator.GetComponentInChildren<Text>().text = "Your Turn";
        yield return new WaitForSeconds(2f);
        StartCoroutine(player.PlayTurn());
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.L)) {
            EnemyTurn();
        }
        */
    }
}
