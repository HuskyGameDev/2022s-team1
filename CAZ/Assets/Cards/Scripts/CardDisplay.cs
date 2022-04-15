using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text nameText;
    public Text descriptionText;
    public Text effectText;
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
        if (card.type == Types.Creature)
        {
            nameText.text = card.name;
            descriptionText.text = card.description;
            artwork.sprite = card.art;
            attackNumText.text = card.attack.ToString();
            defenseNumText.text = card.defense.ToString();
        }
        else if (card.type == Types.Boss)
        {
            nameText.text = card.name;
            descriptionText.text = card.description;
            effectText.text = card.effect;
            artwork.sprite = card.art;
            attackNumText.text = card.attack.ToString();
            defenseNumText.text = card.defense.ToString();
        }
        else if (card.type == Types.Effect)
        {
            nameText.text = card.name;
            descriptionText.text = card.description;
            effectText.text = card.effect;
            artwork.sprite = card.art;
        }
    }

    public void Project(Card c) {
        if (card.type == Types.Creature)
        {
            nameText.text = c.name;
            descriptionText.text = c.description;
            artwork.sprite = c.art;
            attackNumText.text = c.attack.ToString();
            defenseNumText.text = c.defense.ToString();
        }
        else if (card.type == Types.Boss)
        {
            nameText.text = c.name;
            descriptionText.text = c.description;
            effectText.text = c.effect;
            artwork.sprite = c.art;
            attackNumText.text = c.attack.ToString();
            defenseNumText.text = c.defense.ToString();
        }
        else if (card.type == Types.Effect)
        {
            nameText.text = c.name;
            descriptionText.text = c.description;
            effectText.text = c.effect;
            artwork.sprite = c.art;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Display();
    }

    

    
}
