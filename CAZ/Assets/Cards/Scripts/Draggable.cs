using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum Owner { PLAYER, ENEMY };
    public Owner owner = Owner.PLAYER;

    Vector2 posOffset;
    Canvas canvas;
    CursorController cursorController;
    EncounterManager manager;
    public Transform parentToReturnTo = null;
    public bool placed = false;


    private void Start()
    {
        canvas = FindObjectOfType<Canvas>(); // reference canvas for drag/drop
        cursorController = FindObjectOfType<CursorController>();
        manager = FindObjectOfType<EncounterManager>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (owner == Owner.PLAYER && !placed)
        {
            posOffset = eventData.position - (Vector2)this.transform.position;

            parentToReturnTo = this.transform.parent;
            this.transform.SetParent(canvas.transform);

            GetComponent<CanvasGroup>().blocksRaycasts = false;

            //cursorController.cursorImage.sprite = cursorController.grabCursor;
        }
        else if (owner == Owner.PLAYER && placed && this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.BattleReady && this.gameObject.GetComponent<CardDisplay>().card.turnAction == TurnAction.NotUsed && manager.state == BattleState.PLAYERTRUN)
        {
            //change cursor to attack
            cursorController.cursorImage.sprite = cursorController.attackCursor;
            cursorController.cursorState = CursorState.ATTACK;

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (owner == Owner.PLAYER && !placed)
        {
            this.transform.position = eventData.position - posOffset;
        }
        else if (owner == Owner.PLAYER && placed)
        {
            //upadte line

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (owner == Owner.PLAYER)
        {
            this.transform.SetParent(parentToReturnTo);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            //cursorController.cursorImage.sprite = cursorController.normalCursor;
        }
        else if (owner == Owner.PLAYER && placed)
        {
            //attack? - maybe done in dropZone
            //change cursor to normal
            cursorController.cursorImage.sprite = cursorController.normalCursor;
            cursorController.cursorState = CursorState.NORMAL;

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (owner == Owner.PLAYER && placed && this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.BattleReady && this.gameObject.GetComponent<CardDisplay>().card.turnAction == TurnAction.NotUsed && manager.state == BattleState.PLAYERTRUN)
        {
            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(true);
        }

        if (owner == Owner.ENEMY && placed && manager.state == BattleState.PLAYERTRUN && cursorController.cursorState == CursorState.ATTACK)
        {
            this.gameObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(true);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (owner == Owner.PLAYER && placed && this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.BattleReady && manager.state == BattleState.PLAYERTRUN)
        {
            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false);
        }

        if (owner == Owner.ENEMY && placed && manager.state == BattleState.PLAYERTRUN && cursorController.cursorState == CursorState.ATTACK)
        {
            this.gameObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(false);
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Draggable>();

        Draggable drag = eventData.pointerDrag.GetComponent<Draggable>();

        if (drag != null && drag.placed && drag.owner == Owner.PLAYER && this.owner == Owner.ENEMY && cursorController.cursorState == CursorState.ATTACK)
        {
            this.gameObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(false);
            manager.player.attack(drag.GetComponent<CardDisplay>().card, this.gameObject.GetComponent<CardDisplay>().card);
            drag.GetComponent<CardDisplay>().card.turnAction = TurnAction.Used;
        }

    }
}
