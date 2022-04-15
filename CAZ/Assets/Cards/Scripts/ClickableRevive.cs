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
        CardDisplay clickedCard = eventData.pointerClick.GetComponent<CardDisplay>(); // get clicked object
        if (manager.activeEffect == ActiveEffect.REVIVE && manager.state == BattleState.PLAYERTRUN)
        {
            Card revivedCard = Instantiate(clickedCard.card); // create copy of clicked card
            manager.player.hand.Add(revivedCard); // add revived card to hand
            manager.player.RenderCard(revivedCard); // redner revived card in hand
            Card cardInDiscarded = manager.player.discarded.Find((c) => c.name == revivedCard.name); // find revived card in discard pile list
            manager.player.discarded.Remove(cardInDiscarded); // remove revived card from discard pile list
            Debug.Log("Player revives " + revivedCard.name);

            manager.activeEffect = ActiveEffect.NONE; // reset active effect
            Destroy(clickedCard.gameObject); // destroy card object in discard view
            manager.playerDiscardController.DisableDiscardView(); // disable discard view

        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (manager.activeEffect == ActiveEffect.REVIVE && manager.state == BattleState.PLAYERTRUN)
        {
            manager.cursorController.cursorImage.sprite = manager.cursorController.effectCursor; // set cursor to effect sprite
            manager.cursorController.cursorState = CursorState.EFFECT; // set cursor state

            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(true); // enable select card overlay
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (manager.activeEffect == ActiveEffect.REVIVE && manager.state == BattleState.PLAYERTRUN)
        {
            manager.cursorController.cursorImage.sprite = manager.cursorController.normalCursor; // reset cursor to normal sprite
            manager.cursorController.cursorState = CursorState.NORMAL; // set cursor state

            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false); // disable select card overlay
        }
    }
}
