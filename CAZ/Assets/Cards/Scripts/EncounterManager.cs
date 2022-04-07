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

    public List<Card> enemyField;
    public List<Transform> enemyFieldSlots;
    public List<int> enemyFieldSlotAvailability;
    //public Transform enemyFieldSlot1;
    //public Transform enemyFieldSlot2;
    //public Transform enemyFieldSlot3;
    public Transform enemyEffectSlot;
    public Text handNum;
    //public Dictionary<Transform, int> enemyFieldSlots;

    public AI_Standard enemy;
    public PlayerUnit player;
    public CardEffects effects;
    public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(StartBattle());
    }

    IEnumerator StartBattle() {
        //Shuffle Decks
        //Flip Coin for first turn
        //Draw Cards for both player and enemy

        Debug.Log("Starting Battle");

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    void EnemyTurn() {
        // call enemy turn script
        StartCoroutine(enemy.PlayTurn());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            EnemyTurn();
        }
    }
}
