using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum Owner { PLAYER, ENEMY };
    public Owner owner = Owner.PLAYER;

    Vector2 posOffset;
    Canvas canvas;
    public Transform parentToReturnTo = null;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (owner == Owner.PLAYER)
        {
            posOffset = eventData.position - (Vector2)this.transform.position;

            parentToReturnTo = this.transform.parent;
            this.transform.SetParent(canvas.transform);

            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (owner == Owner.PLAYER)
        {
            this.transform.position = eventData.position - posOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (owner == Owner.PLAYER)
        {
            this.transform.SetParent(parentToReturnTo);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}
