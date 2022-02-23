using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TextMesh nameText;
    public TextMesh descriptionText;
    public Image artwork;
    public TextMesh attackNumText;
    public TextMesh defenseNumText;

    /**
     * Updates a card's information in the game view during runtime
     */
    void Display()
    {
        nameText.text = card.name;
        descriptionText.text = card.description;
        artwork.sprite = card.art;
        attackNumText.text = card.attack.ToString();
        defenseNumText.text = card.defense.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        Display();
    }

    

    
}
