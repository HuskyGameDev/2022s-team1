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

            if (zoneType == ZoneType.Creature && (drag.GetComponent<CardDisplay>().card.type == Types.Creature || drag.GetComponent<CardDisplay>().card.type == Types.Boss) && manager.state == BattleState.PLAYERTRUN)
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
            else if (zoneType == ZoneType.Effect && drag.GetComponent<CardDisplay>().card.type == Types.Effect && manager.state == BattleState.PLAYERTRUN)
            {
                /*if (zoneOwner == drag.owner && !taken && !fromTakenParent)
                { // check card and drop zone prereqs
                    drag.parentToReturnTo = this.transform; // set parent to return to to this dropzone
                    drag.GetComponent<CardDisplay>().card.fieldIndex = index; // set field index for reference when destroying cards
                    manager.player.hand.Remove(drag.GetComponent<CardDisplay>().card); // remove card from player's hand
                    taken = true; // mark the dropzone as taken
                    drag.placed = true;*/

                    //manager.cursorController.cursorImage.sprite = manager.cursorController.effectCursor;
                    //manager.cursorController.cursorState = CursorState.EFFECT;

                string effectName = drag.GetComponent<CardDisplay>().card.name;
                switch (effectName) {
                    case "Healing Potion":
                        manager.activeEffect = ActiveEffect.HEALING_POTION;
                        manager.effects.HealingPotion("Player");
                        break;
                    case "Sleight of Hand":
                        manager.activeEffect = ActiveEffect.SLEIGHT_OF_HAND;
                        manager.effects.SleightOfHand("Player");
                        break;
                    case "Sacrifice":
                        manager.activeEffect = ActiveEffect.SACRIFICE;
                        manager.effects.Sacrifice("Player", 0);
                        break;
                    case "Shadow Strike":
                        manager.activeEffect = ActiveEffect.SHADOW_STRIKE;
                        manager.effects.ShadowStrike("Player", 0);
                        break;
                    case "Aggression":
                        manager.activeEffect = ActiveEffect.AGGRESSION;
                        manager.effects.Aggression("Player", 0);
                        break;
                    case "Shield":
                        manager.activeEffect = ActiveEffect.SHIELD;
                        manager.effects.Shield("Player", 0);
                        break;
                    case "Revive":
                        if (manager.player.discarded.Count > 0)  // check if playing revive is valid (avoids soft-lock in revive screen)
                        {
                            if (zoneOwner == drag.owner && !taken && !fromTakenParent) // check card drop prereqs
                            {
                                effectHelper(drag); // perform drag maintenence
                                manager.activeEffect = ActiveEffect.REVIVE; // set active effect
                                manager.effects.Revive("Player", 0); // perform revive
                                manager.player.EraseCard(drag.GetComponent<CardDisplay>().card); // erase revive card
                                taken = false; // free effect slot
                            }
                        }
                        break;
                }
                //}
                //manager.player.EraseCard(drag.GetComponent<CardDisplay>().card);
                //taken = false;

                manager.cursorController.cursorImage.sprite = manager.cursorController.normalCursor;
                manager.cursorController.cursorState = CursorState.NORMAL;
            }
        }
    }

    // performs maintentence tasks for dragging an effect card into the effect card slot
    void effectHelper(Draggable drag) {

        drag.parentToReturnTo = this.transform; // set parent to return to to this dropzone
        drag.GetComponent<CardDisplay>().card.fieldIndex = index; // set field index for reference when destroying cards
        manager.player.hand.Remove(drag.GetComponent<CardDisplay>().card); // remove card from player's hand
        taken = true; // mark the dropzone as taken
        drag.placed = true;

    }
}
