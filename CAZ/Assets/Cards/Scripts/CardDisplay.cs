using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text nameText;
    public Text descriptionText;
    public Image artwork;
    public Text attackNumText;
    public Text defenseNumText;

    public GameObject summonSickOverlay;
    public GameObject attackSelectOverlay;
    public GameObject playerSelectOverlay;

    /**
     * Updates a card's information in the game view during runtime
     */
    public void Display()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;
        artwork.sprite = card.art;
        attackNumText.text = card.attack.ToString();
        defenseNumText.text = card.defense.ToString();
    }

    public void Project(Card c) {
        nameText.text = c.name;
        descriptionText.text = c.description;
        artwork.sprite = c.art;
        attackNumText.text = c.attack.ToString();
        defenseNumText.text = c.defense.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Display();
    }

    

    
}
