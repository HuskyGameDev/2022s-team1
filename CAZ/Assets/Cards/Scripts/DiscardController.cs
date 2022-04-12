using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardController : MonoBehaviour
{
    public GameObject discardView;
    public GameObject discardContent;
    public GameObject cardInDiscard_Prefab;

    //enables discard view for discard view button
    public void EnableDiscardView()
    {
        discardView.SetActive(true);
    }

    //disables discard view for discard view button
    public void DisableDiscardView()
    {
        discardView.SetActive(false);
    }

    public void addCardToContent(Card card) {
        Instantiate(cardInDiscard_Prefab, discardContent.transform);
        cardInDiscard_Prefab.GetComponent<CardDisplay>().card = card;
        cardInDiscard_Prefab.GetComponent<CardDisplay>().Display();
    }
}
