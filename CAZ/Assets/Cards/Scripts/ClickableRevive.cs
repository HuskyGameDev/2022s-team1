using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableRevive : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    EncounterManager manager;

    private void Start()
    {
        manager = FindObjectOfType<EncounterManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardDisplay clickedCard = eventData.pointerClick.GetComponent<CardDisplay>();
        if (manager.activeEffect == ActiveEffect.REVIVE)
        {
            Card revivedCard = Instantiate(clickedCard.card);
            manager.player.hand.Add(revivedCard);
            manager.player.RenderCard(revivedCard);
            Debug.Log("Player revives " + revivedCard.name);

            manager.activeEffect = ActiveEffect.NONE;
            //manager.cursorController.cursorImage.sprite = manager.cursorController.normalCursor;
            //manager.cursorController.cursorState = CursorState.NORMAL;
            Destroy(clickedCard);
            manager.playerDiscardController.DisableDiscardView();

        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (manager.activeEffect == ActiveEffect.REVIVE)
        {
            manager.cursorController.cursorImage.sprite = manager.cursorController.effectCursor;
            manager.cursorController.cursorState = CursorState.EFFECT;
            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(true);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (manager.activeEffect == ActiveEffect.REVIVE)
        {
            manager.cursorController.cursorImage.sprite = manager.cursorController.normalCursor;
            manager.cursorController.cursorState = CursorState.NORMAL;
            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false);
        }
    }
}
