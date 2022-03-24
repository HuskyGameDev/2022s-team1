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
    public List<Card> deck;
    public List<Card> hand;
    public List<Card> discarded;
    public Card bestNextCard;

    public EncounterManager manager;
    public PlayerUnit player;

    public void PlayTurn() {
        PrintDeck();
        DrawCards(); // Draw Cards
        PrintHand();
        PrintDeck();
        DetermineMove();
        PrintField();
        player.PrintField();
        PerformFieldTasks();
        player.PrintField();
    }
    void DrawCards()
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
        Debug.Log("Enemy Starts Attack Round!!!");
        Attacks();
    }

    void Attacks() {
        Card target = null;
        int tempAttackScore = 0;
        int attackScore = int.MaxValue;
        foreach (Card c in manager.enemyField) { // all creatures can attack
            foreach (Card p in manager.playerField) { // check each creature in player field
                Debug.Log("Checking if " + c.name + " can destroy " + p.name);
                if (c.attack > p.defense) // if creature can destroy a player's creature
                {
                    Debug.Log(c.name + " can destroy " + p.name + " Checking if this is a good attack");
                    tempAttackScore = c.attack - p.defense;
                    if (tempAttackScore < attackScore)
                    { // check to see if it is optimal
                        attackScore = tempAttackScore;
                        target = p; // select target
                        Debug.Log(c.name + " has targeted " + p.name);
                    }
                    else {
                        Debug.Log(c.name + " does not target " + p.name);
                    }
                }
            }
            if (target != null)
            {
                Debug.Log(c.name + " is attacking " + target.name);
                AttackAndDestroy(c, target); // destroy target card
                target = null;
            }
            else if (target == null)
            {
                Debug.Log(c.name + " cannot attack any player cards this turn");
            }
            attackScore = int.MaxValue; // reset attackScore for next card
        }
    }

    void AttackAndDestroy(Card aggressor, Card receiver) {

        if (aggressor.attack > receiver.defense) { // double check attack/defense values
            manager.playerField.Remove(receiver); // remove from field
            Debug.Log(aggressor.name + " destroys " + receiver.name);
            manager.player.discarded.Add(receiver); // add to discard pile
            Debug.Log(receiver.name + " is sent to the discard pile");
        }
    }

    void TakeDamage() { 
    
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
