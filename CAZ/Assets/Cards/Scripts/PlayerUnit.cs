using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : MonoBehaviour
{
    public int health;
    public List<Card> hand;
    public List<Card> deck;
    public List<Card> discarded;
    public List<Card> markedCards;

    public EncounterManager manager;
    public Transform handZone;


    public IEnumerator PlayTurn() {
        //Draw cards from deck until hand is full - start of turn, automatic
        StartCoroutine(DrawCards());
        //player functions - summon, effects, attacks
        //end turn - button
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator DrawCards()
    {
        Debug.Log("Drawing Player Cards...");
        var buttonColor = manager.indicator.colors;
        buttonColor.normalColor = Color.green;
        manager.indicator.GetComponentInChildren<Text>().text = "Drawing";
        //draw cards from deck, add to the hand until hand is full (max 3)
        while (hand.Count < 3)
        {
            Card drawnCard = Instantiate(deck[deck.Count - 1]);
            hand.Add(drawnCard);
            RenderCard(drawnCard);
            Debug.Log("Player Draws " + drawnCard.name);
            deck.RemoveAt(deck.Count - 1);
            yield return new WaitForSeconds(0.5f);
            
        }
        manager.indicator.interactable = true;
        manager.indicator.GetComponentInChildren<Text>().text = "End Turn";
        buttonColor.normalColor = Color.blue;
    }
    public void RenderCard(Card card)
    {

        GameObject newCard = Instantiate(card.prefab);
        newCard.transform.SetParent(handZone);
        card.cardObject = newCard;
        card.cardObject.GetComponent<CardDisplay>().card = card;
        card.cardObject.GetComponent<CardDisplay>().Display();

        //newCard.GetComponent<CardDisplay>().card = card;
        //newCard.GetComponent<CardDisplay>().Display();
    }

    public void EraseCard(Card card)
    {
        Destroy(card.cardObject);
    }

    public void attack(Card playerCard, Card enemyCard) {
        Debug.Log(playerCard.name + " attacks " + enemyCard.name + "!");

        if (playerCard.attack > enemyCard.defense)
        { // double check attack/defense values
            manager.enemyField.Remove(enemyCard); // remove from field
            Debug.Log(playerCard.name + " destroys " + enemyCard.name);
            
            manager.enemyAvailableFieldSlots++;
            manager.enemyFieldSlotAvailability[enemyCard.fieldIndex] = 0;
            manager.enemy.discarded.Add(enemyCard); // add to discard pile
            manager.enemyDiscardController.addCardToContent(enemyCard);
            EraseCard(enemyCard);
            Debug.Log(enemyCard.name + " is sent to the discard pile");

            int damageRemainder = playerCard.attack - enemyCard.defense;
            //manager.enemy.TakeDamage(damageRemainder); // Uncomment if destroying creature deals damage
        }
        else if (playerCard.attack == enemyCard.defense)
        {
            // Destroy defending card
            manager.enemyField.Remove(enemyCard); // remove from field
            Debug.Log(playerCard.name + " destroys " + enemyCard.name);
            EraseCard(enemyCard);
            manager.enemyAvailableFieldSlots++;
            manager.enemyFieldSlotAvailability[enemyCard.fieldIndex] = 0;
            manager.enemy.discarded.Add(enemyCard); // add to discard pile
            manager.enemyDiscardController.addCardToContent(enemyCard);
            Debug.Log(enemyCard.name + " is sent to the discard pile");

            // Mark attacking card for destruction after attack phase
            markedCards.Add(playerCard);
            playerCard.cardObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(true);
            Debug.Log(playerCard.name + " was wounded in the attack!");
        }
        else if (playerCard.attack < enemyCard.defense) {
            Debug.Log(playerCard.name + " attacked and missed " + enemyCard.name);
        }
    }

    public void DestroyMarkedCards()
    {

        for (int i = 0; i < markedCards.Count; i++)
        {
            manager.playerField.Remove(markedCards[i]);
            //manager.enemyAvailableFieldSlots++;
            //manager.enemyFieldSlotAvailability[markedCards[i].fieldIndex] = 0;
            manager.playerFieldSlots[markedCards[i].fieldIndex].GetComponent<DropZone>().taken = false;
            EraseCard(markedCards[i]);
            Debug.Log(markedCards[i].name + " fell to its wounds and was destroyed!");
            discarded.Add(markedCards[i]);
            manager.playerDiscardController.addCardToContent(markedCards[i]);
            Debug.Log(markedCards[i].name + " was sent to the discard pile");
        }
        markedCards.Clear();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        manager.playerHPText.text = health.ToString();
        Debug.Log("Player takes " + damage + " points of Damage! | Player's HP: " + health);
    }

    public void EndTurn() {
        manager.indicator.GetComponentInChildren<Text>().text = "Ending Turn";
        manager.indicator.interactable = false;

        foreach (Card c in manager.playerField) {
            if (c.summonState == SummonState.SummonSick)
            {
                c.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
                c.summonState = SummonState.BattleReady;
            }
        }
        DestroyMarkedCards();

        manager.EnemyTurn();
    }



    public void PrintHand()
    {
        Debug.Log("Cards in player hand: ");
        for (int i = 0; i < hand.Count; i++)
        {
            Debug.Log("\t" + hand[i].name + " atk:" + hand[i].attack + " def:" + hand[i].defense);
        }
    }

    public void PrintDeck()
    {
        Debug.Log("Cards in player Deck: ");
        for (int i = 0; i < deck.Count; i++)
        {
            Debug.Log("\t" + deck[i].name);
        }
    }

    public void PrintField()
    {
        Debug.Log("Cards in player field: ");
        for (int i = 0; i < manager.playerField.Count; i++)
        {
            Debug.Log("\t" + manager.playerField[i].name);
        }
    }

    
}
