using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Standard : MonoBehaviour
{

    /**
     * Enemy Turn Algorithm:
     * 
     * 1. Draw Cards
     * 2. Determine best move
     *      2.1 how many field slots are open?
     *      2.2 how many creatures in hand?
     *      2.3 IF # of creatures in hand less than open field slots
     *          THEN play all creatures on the field
     *      2.4 IF # of creatures in hand greater than open field slots
     *          THEN calculate FS for next creature than maximizes FS and play creature on field, repeat until field slots are full
     * 4. Perform Field Tasks (attacking, effect cards, etc.)
     * 5. End Turn
     * 
     */

    public int health;
    public int maxHealth;
    public List<Card> deck;
    public List<Card> hand;
    public List<Card> discarded;
    public Card bestNextCard;
    public List<Card> markedCards;

    public EncounterManager manager;
    public PlayerUnit player;

    private void Awake()
    {
        maxHealth = health;
    }

    public void PlayTurn() {
        PrintDeck();
        DrawCards(); // Draw Cards
        PrintHand();
        PrintDeck();
        DetermineMove();
        PrintField();
        player.PrintField();
        PerformFieldTasks();
        PrintField();
        player.PrintField();
    }
    public void DrawCards()
    {
        Debug.Log("Drawing Cards...");
        //draw cards from deck, add to the hand until hand is full (max 3)
        while(hand.Count < 3) {
            Card drawnCard = deck[deck.Count - 1];
            hand.Add(drawnCard);
            Debug.Log("Enemy Draws " + drawnCard.name);
            deck.RemoveAt(deck.Count - 1);
        }
    }

    void DetermineMove() {
        int tempFS = 0; // store most recently calculated Field Score
        int maxFS = int.MinValue; // keep track of highest calculated Field Score

        bool creatureInHand = CreatureInHand();

        while(manager.enemyAvailableFieldSlots > 0 && creatureInHand) { // while there are open field slots

            foreach (Card c in hand) { // Evaluate potential Field Score for each creature in the hand
                if (c.type == Types.Creature) {
                    Debug.Log("Evaluating FS for " + c.name + "...");
                    tempFS = EvaluateFieldScore(c);
                    Debug.Log("FS for " + c.name + " = " + tempFS);
                    if (tempFS > maxFS) {
                        maxFS = tempFS;
                        bestNextCard = c; // keep track of best next card based on max Field Score
                        Debug.Log("Best next card is: " + c.name);
                    }
                }
            }
            PlayCard(bestNextCard);
            creatureInHand = CreatureInHand();
            maxFS = int.MinValue; // reset maxFS
        }
    }

    bool CreatureInHand() {
        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].type == Types.Creature) {
                return true;
            }
        }
        return false;
    }

    void PlayCard(Card card) {
        Debug.Log("Playing " + card.name + " onto the field.");
        hand.Remove(card);
        manager.enemyField.Add(card);
        manager.enemyAvailableFieldSlots--;
        RenderCard(card);
    }

    public void RenderCard(Card card) {
        /*
        GameObject newCard = Instantiate(manager.cardPrefab, manager.enemyFieldSlots[manager.enemyAvailableFieldSlots]);
        newCard.GetComponent<CardDisplay>().card = card;
        newCard.GetComponent<CardDisplay>().Display();
        */

        GameObject newCard = Instantiate(card.prefab, manager.enemyFieldSlots[manager.enemyAvailableFieldSlots]);
        card.cardObject = newCard;
        card.cardObject.GetComponent<CardDisplay>().card = card;
        card.cardObject.GetComponent<CardDisplay>().Display();

        //newCard.GetComponent<CardDisplay>().card = card;
        //newCard.GetComponent<CardDisplay>().Display();
    }

    /**
     * FOR VISUALS OF CARD GAME:
     * NEED TO CHANGE: implementation of enemyField - change to List<GameObject> and reference Card2.0's card variable
     * This will allow you to instantiate and destroy the correct card game object while still having access to card information.
     * Might have to change other List<Card>'s to List<GameObject>'s as well.
     * Big OOF but needs to be done
     * 
     * OR change card scriptable object to include card 2.0 prefab and draw from itself ???
     */
    public void EraseCard(Card card) {
        Destroy(card.cardObject);
    }

    int EvaluateFieldScore(Card card) {
        int FS = 0;
        FS += health;
        FS -= player.health;
        foreach (Card c in manager.enemyField) { 
            FS += 5;
            FS += c.attack;
            FS += c.defense;
        }
        foreach (Card c in manager.playerField)
        {
            FS -= 5;
            FS -= c.attack;
            FS -= c.defense;
        }

        FS += 5;
        FS += card.attack;
        FS += card.defense;

        return FS;
    }

    void PerformFieldTasks() {
        Debug.Log("Enemy Starts Effect Round!!!");
        Effects();
        Debug.Log("Enemy Starts Attack Round!!!");
        Attacks();
    }

    void Effects() {
        //check if effect cards are in hand
        string effectName;
        foreach (Card c in hand) {
            if (c.type == Types.Effect) {
                effectName = c.name;
                switch (effectName) {
                    case "Healing Potion":
                        //check if healing potion is valuable
                        if (health < maxHealth - 5)
                        {
                            manager.effects.HealingPotion("Enemy");
                        }
                        break;
                    case "Sleight of Hand":
                        //check if sleight of hand is valuable
                        if (deck.Count > 6 && hand.Count < 3)
                        {
                            manager.effects.SleightOfHand("Enemy");
                        }
                        break;
                    case "Sacrifice":
                        //check if sacrifice is valuable
                        if (manager.enemyField.Count == 3 && manager.playerField.Count == 3)
                        {
                            manager.effects.Sacrifice("Enemy", 1);
                        }
                        break;
                    case "Shadow Strike":
                        if (manager.playerField.Count >= 1)
                        {
                            manager.effects.ShadowStrike("Enemy", 1);
                        }
                        break;
                    //check if shadow strike is valuable
                    case "Aggression":
                        //check if aggression is valuable
                        float aggroRand = Random.Range(1, 4);
                        if (aggroRand == 3)
                        {
                            manager.effects.Aggression("Enemy", 1);
                        }
                        break;
                    case "Revive":
                        //check if revive is valuable
                        if (discarded.Count > 0 && manager.enemyAvailableFieldSlots > 0)
                        {
                            manager.effects.Revive("Enemy", 1);
                        }
                        break;
                    case "Shield":
                        //check if shield is valuable
                        float shieldRand = Random.Range(1, 4);
                        if (shieldRand == 3)
                        {
                            manager.effects.Shield("Enemy", 1);
                        }
                        break;
                    default:
                        //effect not found
                        Debug.Log("ERROR - Effect card not found");
                        break;
                }
            }
        }
        //check if effect cards are valuable
        //play effect cards
    }

    void Attacks() {
        Card target = null;
        int tempAttackScore = 0;
        int attackScore = int.MaxValue;
        foreach (Card c in manager.enemyField) { // all creatures can attack
            if (manager.playerField.Count > 0)
            {
                foreach (Card p in manager.playerField)
                { // check each creature in player field
                    Debug.Log("Checking if " + c.name + " can destroy " + p.name);
                    if (c.attack >= p.defense) // if creature can destroy a player's creature
                    {
                        Debug.Log(c.name + " can destroy " + p.name + " Checking if this is a good attack");
                        tempAttackScore = c.attack - p.defense;
                        if (tempAttackScore < attackScore)
                        { // check to see if it is optimal
                            attackScore = tempAttackScore;
                            target = p; // select target
                            Debug.Log(c.name + " has targeted " + p.name);
                        }
                        else
                        {
                            Debug.Log(c.name + " does not target " + p.name);
                        }
                    }
                }
                if (target != null)
                {
                    Debug.Log(c.name + " is attacking " + target.name);
                    AttackAndDestroy(c, target); // destroy target card
                    target = null;
                    attackScore = int.MaxValue; // reset attackScore for next card
                    continue;
                }
                else if (target == null)
                {
                    Debug.Log(c.name + " cannot attack any player cards this turn");
                    attackScore = int.MaxValue; // reset attackScore for next card
                    continue;
                }
                //attackScore = int.MaxValue; // reset attackScore for next card
            }
            else {
                Debug.Log(c.name + " attacks directly!");
                DamagePlayer(c.attack);
            }
        }
        DestroyMarkedCards();
    }

    void AttackAndDestroy(Card aggressor, Card receiver) {

        if (aggressor.attack > receiver.defense)
        { // double check attack/defense values
            manager.playerField.Remove(receiver); // remove from field
            Debug.Log(aggressor.name + " destroys " + receiver.name);
            EraseCard(receiver);

            manager.player.discarded.Add(receiver); // add to discard pile
            Debug.Log(receiver.name + " is sent to the discard pile");

            int damageRemainder = aggressor.attack - receiver.defense;
            //DamagePlayer(damageRemainder); // Uncomment if destroying creature deals damage
        }
        else if (aggressor.attack == receiver.defense) {
            // Destroy defending card
            manager.playerField.Remove(receiver); // remove from field
            Debug.Log(aggressor.name + " destroys " + receiver.name);
            EraseCard(receiver);

            manager.player.discarded.Add(receiver); // add to discard pile
            Debug.Log(receiver.name + " is sent to the discard pile");

            // Mark attacking card for destruction after attack phase
            markedCards.Add(aggressor);
            Debug.Log(aggressor.name + " was wounded in the attack!");
        }
        
    }

    void DamagePlayer(int damage) {
        manager.player.TakeDamage(damage);
    }

    public void DestroyMarkedCards() {

        foreach (Card c in markedCards) {
            manager.enemyField.Remove(c);
            EraseCard(c);
            manager.enemyAvailableFieldSlots++;
            Debug.Log(c.name + " fell to its wounds and was destroyed!");
            discarded.Add(c);
            Debug.Log(c.name + " was sent to the discard pile");
        }
        markedCards.Clear();
    }

    void TakeDamage(int damage) {
        health -= damage;
        Debug.Log("Enemy takes " + damage + " points of Damage! | Enemy's HP: " + health);
    }

    public void PrintHand() {
        Debug.Log("Cards in hand: ");
        for (int i = 0; i < hand.Count; i++) {
            Debug.Log("\t" + hand[i].name + " atk:" + hand[i].attack + " def:" + hand[i].defense);
        }
    }

    public void PrintDeck()
    {
        Debug.Log("Cards in Deck: ");
        for (int i = 0; i < deck.Count; i++)
        {
            Debug.Log("\t" + deck[i].name);
        }
    }

    public void PrintField()
    {
        Debug.Log("Cards in field: ");
        for (int i = 0; i < manager.enemyField.Count; i++)
        {
            Debug.Log("\t" + manager.enemyField[i].name);
        }
    }
}
