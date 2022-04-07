using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeckBuilderButtons : MonoBehaviour
{

    //Replace with reference to game manager
    public Deck deck; // Reference to player's deck
    private DeckBuilderManager deckBuilderManager; // Reference to the DeckBulderManager
    public Card InstanceCard; // The Card to be added/removed - info of card to be added/removed from deck

    private void Start()
    {
        deckBuilderManager = FindObjectOfType<DeckBuilderManager>(); // get DeckBulderManager
        deck = FindObjectOfType<Deck>(); // get player's Deck

    }

    

    /*
     * Adds card to deck list, calls function to add card visually to deck builder 
     */
    public void AddCard() {
        Debug.Log("Attempted to Add");
        CardDex.CardEntry entry = deckBuilderManager.dex.cardDex.Find((x) => x.card.name == GetComponentInParent<CardDisplay>().card.name);
        if (entry.isDiscovered)
        {
            InstanceCard = GetComponentInParent<CardDisplay>().card; // set Instance Card - get card info to add to deck
            deck.deck.Add(InstanceCard); // Add card to deck list
            AddCardToDeckList(InstanceCard); // Add card visually to deck builder
            deckBuilderManager.deckScrollView.verticalNormalizedPosition = 1; // adjust scroll view to accomodate new entry
        }
        else {
            Debug.Log("Failed to add, card not discovered");
        }
    }

    /*
     * Add card visually to deck builder
     */
    public void AddCardToDeckList(Card card) {
        GameObject newItem = Instantiate(deckBuilderManager.cardInDeck); // instaniate new CardInDeck prefab
        newItem.transform.SetParent(deckBuilderManager.deckScrollContent.transform, false); // move new CardInDeck into deck scroll view
        Button itemButton = newItem.GetComponentInChildren<Button>(); // get reference to new CardInDeck's button component
        itemButton.GetComponent<DeckBuilderButtons>().InstanceCard = InstanceCard; // Set Instance card to correct card for removing
        Text newItemText = newItem.transform.Find("CardInDeckField").transform.Find("CardInDeckText").gameObject.GetComponent<Text>(); // get reference to new CardInDeck's name text
        newItemText.text = InstanceCard.name; // change name in deck builder deck scroll view

    }

    /*
     * Removes card from deck list, calls function to remove card visually to deck builder 
     */
    public void RemoveCard() {
        deck.deck.Remove(InstanceCard); // Remove card from deck list
        RemoveCardFromDeckList(InstanceCard); // Destroy CardInDeck from deck builder
    }

    /*
     * Remove card visually from deck builder
     */
    public void RemoveCardFromDeckList(Card card)
    {
        Destroy(transform.parent.gameObject); // Destroy CardInDeck from deck builder
    }

    public void DoneButton() {
        SceneManager.LoadScene("CardScriptableObject");
    }
}
