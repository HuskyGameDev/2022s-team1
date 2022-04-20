using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ClickableFieldEffects : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    EncounterManager manager;

    private void Start()
    {
        manager = FindObjectOfType<EncounterManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardDisplay clickedCard = eventData.pointerClick.GetComponent<CardDisplay>(); // get clicked object

        //Shadow Strike
        if (manager.activeEffect == ActiveEffect.SHADOW_STRIKE && this.GetComponent<Draggable>().owner == Draggable.Owner.ENEMY && manager.state == BattleState.PLAYERTRUN)
        {
            Card struckCard = Instantiate(clickedCard.card); // create copy of clicked card
            Card cardInField = manager.enemyField.Find((c) => c.name == struckCard.name); // find struck card in enemy field list
            manager.enemyField.Remove(cardInField); // remove card from enemy field

            //check if clicked card is aggro, if so deactivate aggro
            if (struckCard.aggro)
            {
                struckCard.attack -= manager.effects.aggressionAmount;
                struckCard.aggro = false;
            }
            //check if clicked card is shielded, if so deactivate shield
            if (struckCard.shield)
            {
                struckCard.attack -= manager.effects.shieldAmount;
                struckCard.shield = false;
            }

            manager.enemyAvailableFieldSlots++; // update number of enemy available field slots
            manager.enemyFieldSlotAvailability[cardInField.fieldIndex] = 0; // update enemy field slot availability
            manager.enemy.discarded.Add(struckCard); // add to discard pile
            manager.enemyDiscardController.addCardToContent(struckCard); // add card to discard view
            manager.enemy.EraseCard(struckCard); // erase card
            Debug.Log("You struck " + struckCard.name + " from the shadows!");
            Debug.Log(struckCard.name + " is sent to the discard pile");
            manager.activeEffect = ActiveEffect.NONE; // reset active effect
            manager.indicator.interactable = true; // set indicator button to active - player can now end turn
        }
        //Sacrifice
        else if (manager.activeEffect == ActiveEffect.SACRIFICE && this.GetComponent<Draggable>().owner == Draggable.Owner.PLAYER && this.GetComponent<Draggable>().placed && manager.state == BattleState.PLAYERTRUN)
        {
            Card sacrificialCard = Instantiate(clickedCard.card); // create copy of clicked card
            Card cardInField = manager.playerField.Find((c) => c.name == sacrificialCard.name); // find sacrifical card in player field list
            manager.playerField.Remove(cardInField); // remove card from player field

            //check if clicked card is aggro, if so deactivate aggro
            if (sacrificialCard.aggro) {
                sacrificialCard.attack -= manager.effects.aggressionAmount;
                sacrificialCard.aggro = false;
            }
            //check if clicked card is shielded, if so deactivate shield
            if (sacrificialCard.shield)
            {
                sacrificialCard.attack -= manager.effects.shieldAmount;
                sacrificialCard.shield = false;
            }

            manager.player.discarded.Add(sacrificialCard); // add to discard pile
            manager.playerDiscardController.addCardToContent(sacrificialCard); // add to discard view
            manager.playerFieldSlots[sacrificialCard.fieldIndex].GetComponent<DropZone>().taken = false; // free field index
            manager.enemy.EraseCard(sacrificialCard); // erase card
            Debug.Log("You sacrificed " + sacrificialCard.name + " ...you monster");
            Debug.Log(sacrificialCard.name + " is sent to the discard pile");
            manager.activeEffect = ActiveEffect.NONE; // reset active effect
            manager.indicator.interactable = true; // set indicator button to active - player can now end turn
        }
        //Aggression
        else if (manager.activeEffect == ActiveEffect.AGGRESSION && this.GetComponent<Draggable>().owner == Draggable.Owner.PLAYER && this.GetComponent<Draggable>().placed && !this.GetComponent<CardDisplay>().card.aggro && manager.state == BattleState.PLAYERTRUN)
        {
            Card aggroCard = Instantiate(clickedCard.card); // create copy of clicked card
            Card cardInField = manager.playerField.Find((c) => c.name == aggroCard.name && c.fieldIndex == aggroCard.fieldIndex); // find aggro card in player field list

            if (cardInField.type == Types.Creature) {
                cardInField.attack += manager.effects.aggressionAmount; // increase attack
            }
            else if (cardInField.type == Types.Boss)
            {
                cardInField.attack += manager.effects.aggressionAmountBoss; // increase attack
            }

            cardInField.cardObject.GetComponent<CardDisplay>().Display(); // update card visual on field
            cardInField.aggro = true; // mark as aggro
            Debug.Log("You taunted " + aggroCard.name + "! They are now angry!");
            Debug.Log(aggroCard.name + "'s attack has increased!");
            manager.activeEffect = ActiveEffect.NONE; // reset active effect

            // prevent overlay overlap
            if (this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.SummonSick)
            {
                this.gameObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
            }
            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false); // disable select card overlay

            manager.indicator.interactable = true; // set indicator button to active - player can now end turn
        }
        //Shield
        else if (manager.activeEffect == ActiveEffect.SHIELD && this.GetComponent<Draggable>().owner == Draggable.Owner.PLAYER && this.GetComponent<Draggable>().placed && !this.GetComponent<CardDisplay>().card.shield && manager.state == BattleState.PLAYERTRUN)
        {
            Card shieldCard = Instantiate(clickedCard.card); // create copy of clicked card
            Card cardInField = manager.playerField.Find((c) => c.name == shieldCard.name && c.fieldIndex == shieldCard.fieldIndex); // find shielded card in player field list

            if (cardInField.type == Types.Creature)
            {
                cardInField.defense += manager.effects.shieldAmount; // increase defense
            }
            else if (cardInField.type == Types.Boss)
            {
                cardInField.defense += manager.effects.shieldAmountBoss; // increase defense
            }

            cardInField.cardObject.GetComponent<CardDisplay>().Display(); // update card visual on field
            cardInField.shield = true; // mark as shielded
            Debug.Log("You shielded " + shieldCard.name + "! They are now more tough!");
            Debug.Log(shieldCard.name + "'s defense has increased!");
            manager.activeEffect = ActiveEffect.NONE; // reset active effect

            // prevent overlay overlap
            if (this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.SummonSick)
            {
                this.gameObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
            }
            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false); // disable select card overlay

            manager.indicator.interactable = true; // set indicator button to active - player can now end turn
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Shadow Strike
        if (manager.activeEffect == ActiveEffect.SHADOW_STRIKE && this.GetComponent<Draggable>().owner == Draggable.Owner.ENEMY && manager.state == BattleState.PLAYERTRUN)
        {
            //manager.cursorController.cursorImage.sprite = manager.cursorController.effectCursor; // set cursor to effect sprite
            //manager.cursorController.cursorState = CursorState.EFFECT; // set cursor state

            this.gameObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(true); // enable select card overlay
        }
        //Sacrifice
        else if (manager.activeEffect == ActiveEffect.SACRIFICE && this.GetComponent<Draggable>().owner == Draggable.Owner.PLAYER && this.GetComponent<Draggable>().placed && manager.state == BattleState.PLAYERTRUN)
        {
            //manager.cursorController.cursorImage.sprite = manager.cursorController.effectCursor; // set cursor to effect sprite
            //manager.cursorController.cursorState = CursorState.EFFECT; // set cursor state

            // prevent overlay overlap
            if (this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.SummonSick) {
                this.gameObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
            }
            this.gameObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(true); // enable select card overlay
        }
        //Aggression
        else if (manager.activeEffect == ActiveEffect.AGGRESSION && this.GetComponent<Draggable>().owner == Draggable.Owner.PLAYER && this.GetComponent<Draggable>().placed && !this.GetComponent<CardDisplay>().card.aggro && manager.state == BattleState.PLAYERTRUN)
        {
            //manager.cursorController.cursorImage.sprite = manager.cursorController.effectCursor; // set cursor to effect sprite
            //manager.cursorController.cursorState = CursorState.EFFECT; // set cursor state

            // prevent overlay overlap
            if (this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.SummonSick)
            {
                this.gameObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
            }
            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(true); // enable select card overlay
        }
        //Shield
        else if (manager.activeEffect == ActiveEffect.SHIELD && this.GetComponent<Draggable>().owner == Draggable.Owner.PLAYER && this.GetComponent<Draggable>().placed && !this.GetComponent<CardDisplay>().card.shield && manager.state == BattleState.PLAYERTRUN)
        {
            //manager.cursorController.cursorImage.sprite = manager.cursorController.effectCursor; // set cursor to effect sprite
            //manager.cursorController.cursorState = CursorState.EFFECT; // set cursor state

            // prevent overlay overlap
            if (this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.SummonSick)
            {
                this.gameObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
            }
            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(true); // enable select card overlay
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Shadow Strike
        if (manager.activeEffect == ActiveEffect.SHADOW_STRIKE && this.GetComponent<Draggable>().owner == Draggable.Owner.ENEMY && manager.state == BattleState.PLAYERTRUN)
        {
            //manager.cursorController.cursorImage.sprite = manager.cursorController.normalCursor; // reset cursor to normal sprite
            //manager.cursorController.cursorState = CursorState.NORMAL; // set cursor state

            this.gameObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(false); // disable select card overlay
        }
        //Sacrifice
        else if (manager.activeEffect == ActiveEffect.SACRIFICE && this.GetComponent<Draggable>().owner == Draggable.Owner.PLAYER && this.GetComponent<Draggable>().placed && manager.state == BattleState.PLAYERTRUN)
        {
            //manager.cursorController.cursorImage.sprite = manager.cursorController.effectCursor; // set cursor to effect sprite
            //manager.cursorController.cursorState = CursorState.EFFECT; // set cursor state

            // prevent overlay overlap
            if (this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.SummonSick)
            {
                this.gameObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
            }
            this.gameObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(false); // disable select card overlay
        }
        //Aggression
        else if (manager.activeEffect == ActiveEffect.AGGRESSION && this.GetComponent<Draggable>().owner == Draggable.Owner.PLAYER && this.GetComponent<Draggable>().placed && !this.GetComponent<CardDisplay>().card.aggro && manager.state == BattleState.PLAYERTRUN)
        {
            //manager.cursorController.cursorImage.sprite = manager.cursorController.effectCursor; // set cursor to effect sprite
            //manager.cursorController.cursorState = CursorState.EFFECT; // set cursor state

            // prevent overlay overlap
            if (this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.SummonSick)
            {
                this.gameObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
            }
            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false); // disable select card overlay
        }
        //Shield
        else if (manager.activeEffect == ActiveEffect.SHIELD && this.GetComponent<Draggable>().owner == Draggable.Owner.PLAYER && this.GetComponent<Draggable>().placed && !this.GetComponent<CardDisplay>().card.shield && manager.state == BattleState.PLAYERTRUN)
        {
            //manager.cursorController.cursorImage.sprite = manager.cursorController.effectCursor; // set cursor to effect sprite
            //manager.cursorController.cursorState = CursorState.EFFECT; // set cursor state

            // prevent overlay overlap
            if (this.gameObject.GetComponent<CardDisplay>().card.summonState == SummonState.SummonSick)
            {
                this.gameObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
            }
            this.gameObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false); // disable select card overlay
        }
    }
}
