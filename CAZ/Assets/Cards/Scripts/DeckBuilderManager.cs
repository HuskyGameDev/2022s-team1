using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilderManager : MonoBehaviour
{

    public GameObject cardInDeck;
    public GameObject deckScrollContent;
    public ScrollRect deckScrollView;
    public Deck deck;

    public CardDex dex;
    public List<GameObject> cardsInView;
    public Card undiscoveredInfo;

    // Start is called before the first frame update
    void Start()
    {
        deck = FindObjectOfType<Deck>();
        dex = FindObjectOfType<CardDex>();
        LoadDeck();
        LoadCards();
    }

    /*
     * Fills deck field UI with all cards currently in player's deck
     */
    public void LoadDeck() {

        foreach (Card card in deck.deck) {
            GameObject newItem = Instantiate(cardInDeck); // instaniate new CardInDeck prefab
            newItem.transform.SetParent(deckScrollContent.transform, false); // move new CardInDeck into deck scroll view
            Button itemButton = newItem.GetComponentInChildren<Button>(); // get reference to new CardInDeck's button component
            itemButton.GetComponent<DeckBuilderButtons>().InstanceCard = card; // Set Instance card to correct card for removing
            Text newItemText = newItem.transform.Find("CardInDeckField").transform.Find("CardInDeckText").gameObject.GetComponent<Text>(); // get reference to new CardInDeck's name text
            newItemText.text = card.name; // change name in deck builder deck scroll view
        }
        
    }

    public void LoadCards() {
        foreach (GameObject civ in cardsInView) {
            string cardName = civ.GetComponent<CardDisplay>().card.name;
            CardDex.CardEntry entry = dex.cardDex.Find((x) => x.card.name == cardName);
            if (entry.card.name == cardName) {
                if (entry.isDiscovered)
                {
                    civ.GetComponent<CardDisplay>().Display();
                }
                else {
                    civ.GetComponent<CardDisplay>().Project(undiscoveredInfo);
                    civ.GetComponentInChildren<Button>().gameObject.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
