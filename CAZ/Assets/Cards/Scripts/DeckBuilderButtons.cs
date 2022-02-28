using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuilderButtons : MonoBehaviour
{

    //Replace with reference to game manager
    public Deck deck;
    public Card card;

    private void Start()
    {

    }

    public void AddCard() {
        deck.deck.Add(card);
    }
}
