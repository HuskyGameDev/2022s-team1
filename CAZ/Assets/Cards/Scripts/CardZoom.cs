using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
    //public CardDisplay display;

    public CardDisplay creatureDisplay;
    public CardDisplay EffectDisplay;
    public CardDisplay BossDisplay;

    public void ActivateZoom(Card card) {
        if (card.type == Types.Creature) {
            creatureDisplay.card = card;
            creatureDisplay.Display();
            creatureDisplay.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
        }
        else if (card.type == Types.Effect)
        {
            EffectDisplay.card = card;
            EffectDisplay.Display();
            EffectDisplay.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
        }
        if (card.type == Types.Boss)
        {
            BossDisplay.card = card;
            BossDisplay.Display();
            BossDisplay.gameObject.SetActive(true);
            this.gameObject.SetActive(true);
        }

    }

    public void DeactivateZoom() {
        this.gameObject.SetActive(false);
        creatureDisplay.gameObject.SetActive(false);
        EffectDisplay.gameObject.SetActive(false);
        BossDisplay.gameObject.SetActive(false);
    }
}
