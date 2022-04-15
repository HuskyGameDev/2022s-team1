using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTRUN, ENEMYTURN, WON, LOST}

public enum ActiveEffect { NONE, HEALING_POTION, SACRIFICE, SHADOW_STRIKE, SLEIGHT_OF_HAND, AGGRESSION, SHIELD, REVIVE }
public class EncounterManager : MonoBehaviour
{

    public BattleState state;
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
    public PlayerUnit player;
    public Button indicator;
    public CardEffects effects;
    public GameObject cardPrefab;
    public CursorController cursorController;
    public DiscardController enemyDiscardController;
    public DiscardController playerDiscardController;
    public CardZoom cardZoom;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        playerHPText.text = player.health.ToString();
        enemyHPText.text = enemy.health.ToString();

        playerDeckText.text = player.deck.Count.ToString();
        enemyDeckText.text = enemy.deck.Count.ToString();

        StartCoroutine(StartBattle());
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
        player.deck.Shuffle(10);
        enemy.deck.Shuffle(10);

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


}
