using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public EncounterManager manager;
    CursorController cursorController;
    Color zoneColor;

    private void Start()
    {
        cursorController = FindObjectOfType<CursorController>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (manager.state == BattleState.PLAYERTRUN && cursorController.cursorState == CursorState.ATTACK && manager.enemyField.Count == 0)
        {
            //this.gameObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(true);
            zoneColor = this.gameObject.GetComponent<Image>().color;
            this.gameObject.GetComponent<Image>().color = Color.red;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (manager.state == BattleState.PLAYERTRUN && cursorController.cursorState == CursorState.ATTACK && manager.enemyField.Count == 0)
        {
            //this.gameObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(false);
            this.gameObject.GetComponent<Image>().color = zoneColor;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Draggable>();

        Draggable drag = eventData.pointerDrag.GetComponent<Draggable>();

        if (drag != null && drag.placed && drag.owner == Draggable.Owner.PLAYER && manager.state == BattleState.PLAYERTRUN && cursorController.cursorState == CursorState.ATTACK && manager.enemyField.Count == 0)
        {
            AudioManager.instance.Play("Card_Attack");
            //this.gameObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(false);
            this.gameObject.GetComponent<Image>().color = zoneColor;
            manager.enemy.TakeDamage(drag.gameObject.GetComponent<CardDisplay>().card.attack);
            drag.GetComponent<CardDisplay>().card.turnAction = TurnAction.Used;
        }
    }
}
