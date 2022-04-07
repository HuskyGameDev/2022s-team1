using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDex : MonoBehaviour
{
    [System.Serializable]
     public class CardEntry {
        public Card card;
        public bool isDiscovered;

        public CardEntry(Card card, bool isDiscovered) {
            this.card = card;
            this.isDiscovered = isDiscovered;
        }
    }

    public List<CardEntry> cardDex;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
