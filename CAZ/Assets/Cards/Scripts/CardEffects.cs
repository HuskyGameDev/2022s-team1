using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardEffects : MonoBehaviour
{
    public EncounterManager manager;
    public int  healAmount = 5;
    public int aggressionAmount = 10;
    public int shieldAmount = 10;

    public void HealingPotion(string flag) {
        switch (flag)
        {
            case "Enemy":
                AudioManager.instance.Play("Card_Effect");
                manager.activeEffect = ActiveEffect.HEALING_POTION;
                manager.enemy.health = manager.enemy.health + healAmount; // heal

                if (manager.enemy.health > manager.enemy.maxHealth) // check if overhealed
                { 
                    manager.enemy.health = manager.enemy.maxHealth; // set to max health
                }

                manager.enemyHPText.text = manager.enemy.health.ToString();
                Debug.Log("Enemy used a health potion!");
                manager.activeEffect = ActiveEffect.NONE; // reset active effect
                break;
            case "Player":
                manager.player.health = manager.player.health + healAmount; // heal

                if (manager.player.health > manager.player.maxHealth) // check if overhealed
                {
                    manager.player.health = manager.player.maxHealth; // set to max health
                }

                manager.playerHPText.text = manager.player.health.ToString();
                manager.activeEffect = ActiveEffect.NONE; // reset active effect
                Debug.Log("You used a health potion!");
                break;
        }
    }

    public void SleightOfHand(string flag)
    {
        switch (flag)
        {
            case "Enemy":
                AudioManager.instance.Play("Card_Effect");
                manager.activeEffect = ActiveEffect.SLEIGHT_OF_HAND;
                Debug.Log("Enemy used a sleight of hand!");
                StartCoroutine(manager.enemy.DrawCards());
                manager.activeEffect = ActiveEffect.NONE; // reset active effect
                break;
            case "Player":
                Debug.Log("Player used sleight of hand!");
                StartCoroutine(manager.player.DrawCards());
                manager.activeEffect = ActiveEffect.NONE; // reset active effect
                break;
        }
    }

    public IEnumerator Sacrifice(string flag, int AINum) {
        switch (flag)
        {
            case "Enemy":
                AudioManager.instance.Play("Card_Effect");
                manager.activeEffect = ActiveEffect.SACRIFICE;
                Card targetCard;
                switch (AINum)
                {
                    case 1:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.NONE);

                        yield return StartCoroutine(SacrificeHelper(targetCard));
                        break;

                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.NONE);

                        yield return StartCoroutine(SacrificeHelper(targetCard));
                        break;

                    case 3:
                        targetCard = GetLowestDefense(manager.enemyField, ActiveEffect.NONE);

                        yield return StartCoroutine(SacrificeHelper(targetCard));
                        break;

                    case 4:
                        targetCard = GetLowestDefense(manager.enemyField, ActiveEffect.NONE);

                        yield return StartCoroutine(SacrificeHelper(targetCard));
                        break;

                    case 5:
                        targetCard = GetLowestAttack(manager.enemyField, ActiveEffect.NONE);

                        yield return StartCoroutine(SacrificeHelper(targetCard));
                        break;

                    case 6:
                        targetCard = GetLowestAttack(manager.enemyField, ActiveEffect.NONE);

                        yield return StartCoroutine(SacrificeHelper(targetCard));
                        break;

                }
                //manager.enemy.DestroyMarkedCards();
                manager.activeEffect = ActiveEffect.NONE; // reset active effect
                break;
            case "Player":
                manager.indicator.interactable = false; // player cannot end turn mid effect
                break;
        }
    }

    IEnumerator SacrificeHelper(Card targetCard) {

        if (targetCard.summonState == SummonState.SummonSick) {
            targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
        }

        yield return new WaitForSeconds(1f);
        targetCard.cardObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(true);
        yield return new WaitForSeconds(1f);
        targetCard.cardObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(false);

        if (targetCard.summonState == SummonState.SummonSick)
        {
            targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
        }

        manager.enemyField.Remove(targetCard);
        manager.RemoveLingeringEffects(targetCard);
        manager.enemy.EraseCard(targetCard);
        manager.enemyAvailableFieldSlots++;
        manager.enemyFieldSlotAvailability[targetCard.fieldIndex] = 0;
        Debug.Log("Enemy Sacrificed " + targetCard.name);
        manager.enemy.discarded.Add(targetCard);
        manager.enemyDiscardController.addCardToContent(targetCard);
        Debug.Log(targetCard.name + " was sent to the discard pile");

    }

    public IEnumerator ShadowStrike(string flag, int AINum)
    {
        switch (flag)
        {
            case "Enemy":
                AudioManager.instance.Play("Card_Effect");
                manager.activeEffect = ActiveEffect.SHADOW_STRIKE;
                Card targetCard;
                switch (AINum)
                {
                    case 1:
                        targetCard = GetRandomCardOnField(manager.playerField, ActiveEffect.NONE);

                        yield return StartCoroutine(ShadowStrikeHelper(targetCard));
                        break;

                    case 2:
                        targetCard = GetRandomCardOnField(manager.playerField, ActiveEffect.NONE);

                        yield return StartCoroutine(ShadowStrikeHelper(targetCard));
                        break;

                    case 3:
                        targetCard = GetHighestDefense(manager.playerField, ActiveEffect.NONE);

                        yield return StartCoroutine(ShadowStrikeHelper(targetCard));
                        break;

                    case 4:
                        targetCard = GetHighestDefense(manager.playerField, ActiveEffect.NONE);

                        yield return StartCoroutine(ShadowStrikeHelper(targetCard));
                        break;

                    case 5:
                        targetCard = GetHighestAttack(manager.playerField, ActiveEffect.NONE);

                        yield return StartCoroutine(ShadowStrikeHelper(targetCard));
                        break;

                    case 6:
                        targetCard = GetHighestAttack(manager.playerField, ActiveEffect.NONE);

                        yield return StartCoroutine(ShadowStrikeHelper(targetCard));
                        break;

                }
                manager.activeEffect = ActiveEffect.NONE; // reset active effect
                break;
            case "Player":
                manager.indicator.interactable = false; // player cannot end turn mid effect
                break;
        }
    }

    IEnumerator ShadowStrikeHelper(Card targetCard) {

        if (targetCard.summonState == SummonState.SummonSick)
        {
            targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
        }

        yield return new WaitForSeconds(1f);
        targetCard.cardObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(true);
        yield return new WaitForSeconds(1f);
        targetCard.cardObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(false);

        if (targetCard.summonState == SummonState.SummonSick)
        {
            targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
        }

        manager.playerField.Remove(targetCard); // remove from field
        manager.RemoveLingeringEffects(targetCard);
        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
        manager.player.discarded.Add(targetCard);
        manager.playerDiscardController.addCardToContent(targetCard);
        manager.enemy.EraseCard(targetCard);
        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
    }
    

    public IEnumerator Aggression(string flag, int AINum)
    {
        switch (flag)
        {
            case "Enemy":
                AudioManager.instance.Play("Card_Effect");
                manager.activeEffect = ActiveEffect.AGGRESSION;
                Card targetCard;
                switch (AINum)
                {
                    case 1:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.AGGRESSION);

                        yield return StartCoroutine(AggressionHelper(targetCard));
                        break;

                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.AGGRESSION);

                        yield return StartCoroutine(AggressionHelper(targetCard));
                        break;

                    case 3:
                        targetCard = GetHighestAttack(manager.enemyField, ActiveEffect.AGGRESSION);

                        yield return StartCoroutine(AggressionHelper(targetCard));
                        break;

                    case 4:
                        targetCard = GetHighestAttack(manager.enemyField, ActiveEffect.AGGRESSION);

                        yield return StartCoroutine(AggressionHelper(targetCard));
                        break;

                    case 5:
                        targetCard = GetLowestAttack(manager.enemyField, ActiveEffect.AGGRESSION);

                        yield return StartCoroutine(AggressionHelper(targetCard));
                        break;

                    case 6:
                        targetCard = GetLowestAttack(manager.enemyField, ActiveEffect.AGGRESSION);

                        yield return StartCoroutine(AggressionHelper(targetCard));
                        break;

                }
                manager.activeEffect = ActiveEffect.NONE; // reset active effect
                break;
            case "Player":
                manager.indicator.interactable = false; // player cannot end turn mid effect
                break;
        }
    }

    IEnumerator AggressionHelper(Card targetCard) {

        if (targetCard.summonState == SummonState.SummonSick)
        {
            targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
        }

        yield return new WaitForSeconds(1f);
        targetCard.cardObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(true);
        yield return new WaitForSeconds(1f);
        targetCard.cardObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false);

        if (targetCard.summonState == SummonState.SummonSick)
        {
            targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
        }

        targetCard.attack += aggressionAmount;
        targetCard.aggro = true;
        targetCard.cardObject.GetComponent<CardDisplay>().Display();
        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
    }

    public IEnumerator Shield(string flag, int AINum)
    {
        switch (flag)
        {
            case "Enemy":
                AudioManager.instance.Play("Card_Effect");
                manager.activeEffect = ActiveEffect.SHIELD;
                Card targetCard;
                switch (AINum)
                {
                    case 1:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.SHIELD);

                        yield return StartCoroutine(ShieldHelper(targetCard));
                        break;

                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.SHIELD);

                        yield return StartCoroutine(ShieldHelper(targetCard));
                        break;

                    case 3:
                        targetCard = GetLowestDefense(manager.enemyField, ActiveEffect.SHIELD);

                        yield return StartCoroutine(ShieldHelper(targetCard));
                        break;

                    case 4:
                        targetCard = GetLowestDefense(manager.enemyField, ActiveEffect.SHIELD);

                        yield return StartCoroutine(ShieldHelper(targetCard));
                        break;

                    case 5:
                        targetCard = GetHighestDefense(manager.enemyField, ActiveEffect.SHIELD);

                        yield return StartCoroutine(ShieldHelper(targetCard));
                        break;

                    case 6:
                        targetCard = GetHighestDefense(manager.enemyField, ActiveEffect.SHIELD);

                        yield return StartCoroutine(ShieldHelper(targetCard));
                        break;

                }
                manager.activeEffect = ActiveEffect.NONE; // reset active effect
                break;
            case "Player":
                manager.indicator.interactable = false; // player cannot end turn mid effect
                // check ClickableFieldEffects for shadow strike implementation
                break;
        }
    }

    IEnumerator ShieldHelper(Card targetCard) {

        if (targetCard.summonState == SummonState.SummonSick)
        {
            targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
        }

        yield return new WaitForSeconds(1f);
        targetCard.cardObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(true);
        yield return new WaitForSeconds(1f);
        targetCard.cardObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false);

        if (targetCard.summonState == SummonState.SummonSick)
        {
            targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
        }

        targetCard.defense += shieldAmount;
        targetCard.shield = true;
        targetCard.cardObject.GetComponent<CardDisplay>().Display();
        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
    }

    public void Revive(string flag, int AINum)
    {
        switch (flag)
        {
            case "Enemy":
                AudioManager.instance.Play("Card_Effect");
                manager.activeEffect = ActiveEffect.REVIVE;
                Card targetCard;
                switch (AINum)
                {
                    case 1:
                        targetCard = GetRandomCardOnField(manager.enemy.discarded, ActiveEffect.NONE);
                        manager.enemy.discarded.Remove(targetCard);

                        // -- Add revived card to field, battle ready --//
                        //manager.enemyField.Add(targetCard);
                        //manager.enemyAvailableFieldSlots--;
                        //render card
                        //manager.enemy.RenderCard(targetCard);
                        //targetCard.summonState = SummonState.BattleReady;
                        //targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);

                        manager.enemy.hand.Add(targetCard);
                        manager.handNum.text = manager.enemy.hand.Count.ToString(); // relay visual info on hands in card;
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemy.discarded, ActiveEffect.NONE);
                        manager.enemy.discarded.Remove(targetCard);

                        // -- Add revived card to field, battle ready --//
                        //manager.enemyField.Add(targetCard);
                        //manager.enemyAvailableFieldSlots--;
                        //render card
                        //manager.enemy.RenderCard(targetCard);
                        //targetCard.summonState = SummonState.BattleReady;
                        //targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);

                        manager.enemy.hand.Add(targetCard);
                        manager.handNum.text = manager.enemy.hand.Count.ToString(); // relay visual info on hands in card;
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                    case 3:
                        targetCard = GetHighestAttack(manager.enemy.discarded, ActiveEffect.NONE);
                        manager.enemy.discarded.Remove(targetCard);

                        // -- Add revived card to field, battle ready --//
                        //manager.enemyField.Add(targetCard);
                        //manager.enemyAvailableFieldSlots--;
                        //render card
                        //manager.enemy.RenderCard(targetCard);
                        //targetCard.summonState = SummonState.BattleReady;
                        //targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);

                        manager.enemy.hand.Add(targetCard);
                        manager.handNum.text = manager.enemy.hand.Count.ToString(); // relay visual info on hands in card;
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                    case 4:
                        targetCard = GetHighestAttack(manager.enemy.discarded, ActiveEffect.NONE);
                        manager.enemy.discarded.Remove(targetCard);

                        // -- Add revived card to field, battle ready --//
                        //manager.enemyField.Add(targetCard);
                        //manager.enemyAvailableFieldSlots--;
                        //render card
                        //manager.enemy.RenderCard(targetCard);
                        //targetCard.summonState = SummonState.BattleReady;
                        //targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);

                        manager.enemy.hand.Add(targetCard);
                        manager.handNum.text = manager.enemy.hand.Count.ToString(); // relay visual info on hands in card;
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                    case 5:
                        targetCard = GetHighestDefense(manager.enemy.discarded, ActiveEffect.NONE);
                        manager.enemy.discarded.Remove(targetCard);

                        // -- Add revived card to field, battle ready --//
                        //manager.enemyField.Add(targetCard);
                        //manager.enemyAvailableFieldSlots--;
                        //render card
                        //manager.enemy.RenderCard(targetCard);
                        //targetCard.summonState = SummonState.BattleReady;
                        //targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);

                        manager.enemy.hand.Add(targetCard);
                        manager.handNum.text = manager.enemy.hand.Count.ToString(); // relay visual info on hands in card;
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                    case 6:
                        targetCard = GetHighestDefense(manager.enemy.discarded, ActiveEffect.NONE);
                        manager.enemy.discarded.Remove(targetCard);

                        // -- Add revived card to field, battle ready --//
                        //manager.enemyField.Add(targetCard);
                        //manager.enemyAvailableFieldSlots--;
                        //render card
                        //manager.enemy.RenderCard(targetCard);
                        //targetCard.summonState = SummonState.BattleReady;
                        //targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);

                        manager.enemy.hand.Add(targetCard);
                        manager.handNum.text = manager.enemy.hand.Count.ToString(); // relay visual info on hands in card;
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                }
                manager.activeEffect = ActiveEffect.NONE; // reset active effect
                break;
            case "Player":
                Debug.Log("Opening discard pile for revive");
                manager.playerDiscardController.EnableDiscardView(false);
                
                break;
        }
    }



    Card GetHighestAttack(List<Card> field, ActiveEffect effect) {
        int tempAttack = int.MinValue;
        Card targetCard = null;

        if (effect == ActiveEffect.AGGRESSION)
        {
            foreach (Card c in field)
            {
                if (c.attack > tempAttack && !c.aggro && c.summonState != SummonState.SummonSick)
                {
                    tempAttack = c.attack;
                    targetCard = c;
                }
            }
        }
        else if (effect == ActiveEffect.SHIELD)
        {
            foreach (Card c in field)
            {
                if (c.attack > tempAttack && !c.shield)
                {
                    tempAttack = c.attack;
                    targetCard = c;
                }
            }
        }
        else {
            foreach (Card c in field)
            {
                if (c.attack > tempAttack)
                {
                    tempAttack = c.attack;
                    targetCard = c;
                }
            }
        }
        return targetCard;
    }

    Card GetLowestAttack(List<Card> field, ActiveEffect effect)
    {
        int tempAttack = int.MaxValue;
        Card targetCard = null;

        if (effect == ActiveEffect.AGGRESSION)
        {
            foreach (Card c in field)
            {
                if (c.attack < tempAttack && !c.aggro && c.summonState != SummonState.SummonSick)
                {
                    tempAttack = c.attack;
                    targetCard = c;
                }
            }
        }
        else if (effect == ActiveEffect.SHIELD)
        {
            foreach (Card c in field)
            {
                if (c.attack < tempAttack && !c.shield)
                {
                    tempAttack = c.attack;
                    targetCard = c;
                }
            }
        }
        else
        {
            foreach (Card c in field)
            {
                if (c.attack < tempAttack)
                {
                    tempAttack = c.attack;
                    targetCard = c;
                }
            }
        }
        return targetCard;
    }

    Card GetHighestDefense(List<Card> field, ActiveEffect effect)
    {
        int tempDefense = int.MinValue;
        Card targetCard = null;

        if (effect == ActiveEffect.AGGRESSION)
        {
            foreach (Card c in field)
            {
                if (c.defense > tempDefense && !c.aggro && c.summonState != SummonState.SummonSick)
                {
                    tempDefense = c.defense;
                    targetCard = c;
                }
            }
        }
        else if (effect == ActiveEffect.SHIELD)
        {
            foreach (Card c in field)
            {
                if (c.defense > tempDefense && !c.shield)
                {
                    tempDefense = c.defense;
                    targetCard = c;
                }
            }
        }
        else {
            foreach (Card c in field)
            {
                if (c.defense > tempDefense)
                {
                    tempDefense = c.defense;
                    targetCard = c;
                }
            }
        }
        return targetCard;
    }


    Card GetLowestDefense(List<Card> field, ActiveEffect effect)
    {
        int tempDefense = int.MaxValue;
        Card targetCard = null;

        if (effect == ActiveEffect.AGGRESSION)
        {
            foreach (Card c in field)
            {
                if (c.defense < tempDefense && !c.aggro && c.summonState != SummonState.SummonSick)
                {
                    tempDefense = c.defense;
                    targetCard = c;
                }
            }
        }
        else if (effect == ActiveEffect.SHIELD)
        {
            foreach (Card c in field)
            {
                if (c.defense < tempDefense && !c.shield)
                {
                    tempDefense = c.defense;
                    targetCard = c;
                }
            }
        }
        else {
            foreach (Card c in field)
            {
                if (c.defense < tempDefense)
                {
                    tempDefense = c.defense;
                    targetCard = c;
                }
            }
        }
        return targetCard;
    }

    Card GetRandomCardOnField(List<Card> field, ActiveEffect effect) {
        bool found = false;
        int index = 0;
        if (effect == ActiveEffect.AGGRESSION)
        {
            while (!found)
            {
                index = Random.Range(0, field.Count);
                if (!field[index].aggro && field[index].summonState != SummonState.SummonSick)
                {
                    found = true;
                }
            }
        }
        else if (effect == ActiveEffect.SHIELD)
        {
            while (!found)
            {
                index = Random.Range(0, field.Count);
                if (!field[index].shield)
                {
                    found = true;
                }
            }
        }
        else {
            index = Random.Range(0, field.Count);
        }

        return field[index];
    }
}
