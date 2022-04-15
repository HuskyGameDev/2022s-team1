using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
    public CardDisplay display;

    public void ActivateZoom(Card card) {
        display.card = card;
        display.Display();
        this.gameObject.SetActive(true);
    }

    public void DeactivateZoom() {
        this.gameObject.SetActive(false);
    }
}
