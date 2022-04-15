using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckBuilderZoomDetect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public DeckBuilderManager manager;
    public CardDex dex;
    bool hovered;
    bool zoomed;
    bool discovered = false;

    private void Start()
    {
        manager = FindObjectOfType<DeckBuilderManager>();
        dex = FindObjectOfType<CardDex>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && hovered && discovered)
        {
            manager.cardZoom.ActivateZoom(this.GetComponent<CardDisplay>().card);
            zoomed = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            manager.cardZoom.DeactivateZoom();
            zoomed = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CardDex.CardEntry entry = dex.cardDex.Find((x) => x.card.name == this.gameObject.GetComponent<CardDisplay>().card.name);
        if (entry.isDiscovered) {
            discovered = true;
        }
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        discovered = false;
        hovered = false;
    }
}
