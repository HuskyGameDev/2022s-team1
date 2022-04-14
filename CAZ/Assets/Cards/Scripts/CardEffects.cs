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
                manager.enemy.health = manager.enemy.health + healAmount;
                manager.enemyHPText.text = manager.enemy.health.ToString();
                Debug.Log("Enemy used a health potion!");
                break;
            case "Player":
                manager.player.health = manager.player.health + healAmount;
                manager.playerHPText.text = manager.player.health.ToString();
                Debug.Log("You used a health potion!");
                break;
        }
    }

    public void SleightOfHand(string flag)
    {
        switch (flag)
        {
            case "Enemy":
                Debug.Log("Enemy used a sleight of hand!");
                StartCoroutine(manager.enemy.DrawCards());
                break;
            case "Player":

                break;
        }
    }

    public void Sacrifice(string flag, int AINum) {
        switch (flag)
        {
            case "Enemy":
                Card targetCard;
                switch (AINum)
                {
                    case 1:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.NONE);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.NONE);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                    case 3:
                        targetCard = GetLowestDefense(manager.enemyField, ActiveEffect.NONE);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                    case 4:
                        targetCard = GetLowestDefense(manager.enemyField, ActiveEffect.NONE);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                    case 5:
                        targetCard = GetLowestAttack(manager.enemyField, ActiveEffect.NONE);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                    case 6:
                        targetCard = GetLowestAttack(manager.enemyField, ActiveEffect.NONE);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                }
                manager.enemy.DestroyMarkedCards();
                break;
            case "Player":
                manager.indicator.interactable = false; // player cannot end turn mid effect
                break;
        }
    }

    public void ShadowStrike(string flag, int AINum)
    {
        switch (flag)
        {
            case "Enemy":
                Card targetCard;
                switch (AINum)
                {
                    case 1:
                        targetCard = GetRandomCardOnField(manager.playerField, ActiveEffect.NONE);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                    case 2:
                        targetCard = GetRandomCardOnField(manager.playerField, ActiveEffect.NONE);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                    case 3:
                        targetCard = GetHighestDefense(manager.playerField, ActiveEffect.NONE);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                    case 4:
                        targetCard = GetHighestDefense(manager.playerField, ActiveEffect.NONE);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                    case 5:
                        targetCard = GetHighestAttack(manager.playerField, ActiveEffect.NONE);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                    case 6:
                        targetCard = GetHighestAttack(manager.playerField, ActiveEffect.NONE);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                }
                break;
            case "Player":
                manager.indicator.interactable = false; // player cannot end turn mid effect
                break;
        }
    }

    public void Aggression(string flag, int AINum)
    {
        switch (flag)
        {
            case "Enemy":
                Card targetCard;
                switch (AINum)
                {
                    case 1:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.AGGRESSION);
                        targetCard.attack += aggressionAmount;
                        targetCard.aggro = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.AGGRESSION);
                        targetCard.attack += aggressionAmount;
                        targetCard.aggro = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                    case 3:
                        targetCard = GetHighestAttack(manager.enemyField, ActiveEffect.AGGRESSION);
                        targetCard.attack += aggressionAmount;
                        targetCard.aggro = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                    case 4:
                        targetCard = GetHighestAttack(manager.enemyField, ActiveEffect.AGGRESSION);
                        targetCard.attack += aggressionAmount;
                        targetCard.aggro = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                    case 5:
                        targetCard = GetLowestAttack(manager.enemyField, ActiveEffect.AGGRESSION);
                        targetCard.attack += aggressionAmount;
                        targetCard.aggro = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                    case 6:
                        targetCard = GetLowestAttack(manager.enemyField, ActiveEffect.AGGRESSION);
                        targetCard.attack += aggressionAmount;
                        targetCard.aggro = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                }
                break;
            case "Player":
                manager.indicator.interactable = false; // player cannot end turn mid effect
                break;
        }
    }

    public void Shield(string flag, int AINum)
    {
        switch (flag)
        {
            case "Enemy":
                Card targetCard;
                switch (AINum)
                {
                    case 1:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.SHIELD);
                        targetCard.defense += shieldAmount;
                        targetCard.shield = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemyField, ActiveEffect.SHIELD);
                        targetCard.defense += shieldAmount;
                        targetCard.shield = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                    case 3:
                        targetCard = GetLowestDefense(manager.enemyField, ActiveEffect.SHIELD);
                        targetCard.defense += shieldAmount;
                        targetCard.shield = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                    case 4:
                        targetCard = GetLowestDefense(manager.enemyField, ActiveEffect.SHIELD);
                        targetCard.defense += shieldAmount;
                        targetCard.shield = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                    case 5:
                        targetCard = GetHighestDefense(manager.enemyField, ActiveEffect.SHIELD);
                        targetCard.defense += shieldAmount;
                        targetCard.shield = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                    case 6:
                        targetCard = GetHighestDefense(manager.enemyField, ActiveEffect.SHIELD);
                        targetCard.defense += shieldAmount;
                        targetCard.shield = true;
                        targetCard.cardObject.GetComponent<CardDisplay>().Display();
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                }
                break;
            case "Player":
                manager.indicator.interactable = false; // player cannot end turn mid effect
                // check ClickableFieldEffects for shadow strike implementation
                break;
        }
    }

    public void Revive(string flag, int AINum)
    {
        switch (flag)
        {
            case "Enemy":
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
                if (c.attack > tempAttack && !c.aggro)
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
                if (c.attack < tempAttack && !c.aggro)
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
                if (c.defense > tempDefense && !c.aggro)
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
                if (c.defense < tempDefense && !c.aggro)
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
                if (!field[index].aggro)
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
