using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDex : MonoBehaviour
{
    [System.Serializable]
     public class CardEntry {
        public Card card;
        public bool isDiscovered;
        public int countInDeck;

        public CardEntry(Card card, bool isDiscovered, int countInDeck) {
            this.card = card;
            this.isDiscovered = isDiscovered;
            this.countInDeck = countInDeck;
        }
    }

    public List<CardEntry> cardDex;

    void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }
}
