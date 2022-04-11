using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public enum ZoneType { Creature, Effect };

    public ZoneType zoneType;
    public Draggable.Owner zoneOwner;
    public bool taken = false;
    public int index;
    bool fromTakenParent = true;
    public EncounterManager manager;

    public void OnPointerEnter(PointerEventData eventData)
    { 
    
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Draggable>();

        Draggable drag = eventData.pointerDrag.GetComponent<Draggable>();
        if (drag != null && !drag.placed)
        {
            if (drag.parentToReturnTo.GetComponent<DropZone>() == null)
            { // check to see if the card is coming from the hand zone
                fromTakenParent = false;
            }

            if (zoneType == ZoneType.Creature && (drag.GetComponent<CardDisplay>().card.type == Types.Creature || drag.GetComponent<CardDisplay>().card.type == Types.Boss))
            {
                if (zoneOwner == drag.owner && !taken && !fromTakenParent)
                { // check card and drop zone prereqs
                    drag.parentToReturnTo = this.transform; // set parent to return to to this dropzone
                    drag.GetComponent<CardDisplay>().card.fieldIndex = index; // set field index for reference when destroying cards
                    manager.player.hand.Remove(drag.GetComponent<CardDisplay>().card); // remove card from player's hand
                    manager.playerField.Add(drag.GetComponent<CardDisplay>().card); // add card to player's field
                    drag.GetComponent<CardDisplay>().card.summonState = SummonState.SummonSick;
                    //drag.transform.Find("SummonSickOverlay").gameObject.SetActive(true);
                    drag.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
                    taken = true; // mark the dropzone as taken
                    drag.placed = true;
                }
            }
            else if (zoneType == ZoneType.Effect && drag.GetComponent<CardDisplay>().card.type == Types.Effect)
            {
                if (zoneOwner == drag.owner && !taken && !fromTakenParent)
                { // check card and drop zone prereqs
                    drag.parentToReturnTo = this.transform; // set parent to return to to this dropzone
                    drag.GetComponent<CardDisplay>().card.fieldIndex = index; // set field index for reference when destroying cards
                    manager.player.hand.Remove(drag.GetComponent<CardDisplay>().card); // remove card from player's hand
                    taken = true; // mark the dropzone as taken
                    drag.placed = true;
                }
            }
        }
    }
}
