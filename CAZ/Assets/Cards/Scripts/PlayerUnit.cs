using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public int health;
    public List<Card> hand;
    public List<Card> deck;
    public List<Card> discarded;

    public EncounterManager manager;

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
