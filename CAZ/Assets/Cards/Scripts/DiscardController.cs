using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardController : MonoBehaviour
{
    public GameObject discardView;
    public GameObject discardContent;
    public GameObject cardInDiscard_Prefab;
    public GameObject closeButton;

    //enables discard view for discard view button
    public void EnableDiscardView(bool withButton)
    {
        if (withButton)
        {
            closeButton.SetActive(true);
            discardView.SetActive(true);
        }
        else if (!withButton) {
            closeButton.SetActive(false);
            discardView.SetActive(true);
        }
    }

    //disables discard view for discard view button
    public void DisableDiscardView()
    {
        closeButton.SetActive(true);
        discardView.SetActive(false);
    }

    public void addCardToContent(Card card) {
        GameObject destroyedCard = Instantiate(cardInDiscard_Prefab, discardContent.transform);
        destroyedCard.GetComponent<CardDisplay>().card = card;
        destroyedCard.GetComponent<CardDisplay>().Display();
    }
}
