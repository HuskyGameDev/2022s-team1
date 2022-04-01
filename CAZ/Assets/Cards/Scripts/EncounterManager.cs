using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //public Transform enemyFieldSlot1;
    //public Transform enemyFieldSlot2;
    //public Transform enemyFieldSlot3;
    public Transform enemyEffectSlot;

    public AI_Standard enemy;
    public PlayerUnit player;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartBattle();
    }

    void StartBattle() {
        //Shuffle Decks
        //Flip Coin for first turn
        //Draw Cards for both player and enemy

        Debug.Log("Starting Battle");

        state = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    void EnemyTurn() {
        // call enemy turn script
        enemy.PlayTurn();
    }
}
