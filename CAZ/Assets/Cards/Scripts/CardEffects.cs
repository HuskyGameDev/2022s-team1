using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Debug.Log("Enemy used a health potion!");
                break;
            case "Player":
                manager.player.health = manager.player.health + healAmount;
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
                        targetCard = GetRandomCardOnField(manager.enemyField);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemyField);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                    case 3:
                        targetCard = GetLowestDefense(manager.enemyField);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                    case 4:
                        targetCard = GetLowestDefense(manager.enemyField);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                    case 5:
                        targetCard = GetLowestAttack(manager.enemyField);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                    case 6:
                        targetCard = GetLowestAttack(manager.enemyField);
                        manager.enemy.markedCards.Add(targetCard);
                        Debug.Log("Enemy Sacrificed " + targetCard.name);
                        break;
                }
                manager.enemy.DestroyMarkedCards();
                break;
            case "Player":

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
                        targetCard = GetRandomCardOnField(manager.playerField);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                    case 2:
                        targetCard = GetRandomCardOnField(manager.playerField);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                    case 3:
                        targetCard = GetHighestDefense(manager.playerField);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                    case 4:
                        targetCard = GetHighestDefense(manager.playerField);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                    case 5:
                        targetCard = GetHighestAttack(manager.playerField);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                    case 6:
                        targetCard = GetHighestAttack(manager.playerField);
                        manager.playerField.Remove(targetCard); // remove from field
                        manager.playerFieldSlots[targetCard.fieldIndex].GetComponent<DropZone>().taken = false;
                        manager.enemy.EraseCard(targetCard);
                        Debug.Log("Enemy Struck " + targetCard.name + " from the shadows!");
                        break;
                }
                break;
            case "Player":

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
                        targetCard = GetRandomCardOnField(manager.enemyField);
                        targetCard.attack += aggressionAmount;
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemyField);
                        targetCard.attack += aggressionAmount;
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                    case 3:
                        targetCard = GetHighestAttack(manager.enemyField);
                        targetCard.attack += aggressionAmount;
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                    case 4:
                        targetCard = GetHighestAttack(manager.enemyField);
                        targetCard.attack += aggressionAmount;
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                    case 5:
                        targetCard = GetLowestAttack(manager.enemyField);
                        targetCard.attack += aggressionAmount;
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                    case 6:
                        targetCard = GetLowestAttack(manager.enemyField);
                        targetCard.attack += aggressionAmount;
                        Debug.Log("Enemy's " + targetCard.name + " becomes aggressive!");
                        break;
                }
                break;
            case "Player":

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
                        targetCard = GetRandomCardOnField(manager.enemyField);
                        targetCard.defense += shieldAmount;
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemyField);
                        targetCard.defense += shieldAmount;
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                    case 3:
                        targetCard = GetLowestDefense(manager.enemyField);
                        targetCard.defense += shieldAmount;
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                    case 4:
                        targetCard = GetLowestDefense(manager.enemyField);
                        targetCard.defense += shieldAmount;
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                    case 5:
                        targetCard = GetHighestDefense(manager.enemyField);
                        targetCard.defense += shieldAmount;
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                    case 6:
                        targetCard = GetHighestDefense(manager.enemyField);
                        targetCard.defense += shieldAmount;
                        Debug.Log("Enemy's " + targetCard.name + " gets shielded!");
                        break;
                }
                break;
            case "Player":

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
                        targetCard = GetRandomCardOnField(manager.enemy.discarded);
                        manager.enemy.discarded.Remove(targetCard);
                        manager.enemyField.Add(targetCard);
                        manager.enemyAvailableFieldSlots--;
                        //render card
                        manager.enemy.RenderCard(targetCard);
                        targetCard.summonState = SummonState.BattleReady;
                        targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                    case 2:
                        targetCard = GetRandomCardOnField(manager.enemy.discarded);
                        manager.enemy.discarded.Remove(targetCard);
                        manager.enemyField.Add(targetCard);
                        manager.enemyAvailableFieldSlots--;
                        //render card
                        manager.enemy.RenderCard(targetCard);
                        targetCard.summonState = SummonState.BattleReady;
                        targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                    case 3:
                        targetCard = GetHighestAttack(manager.enemy.discarded);
                        manager.enemy.discarded.Remove(targetCard);
                        manager.enemyField.Add(targetCard);
                        manager.enemyAvailableFieldSlots--;
                        //render card
                        manager.enemy.RenderCard(targetCard);
                        targetCard.summonState = SummonState.BattleReady;
                        targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                    case 4:
                        targetCard = GetHighestAttack(manager.enemy.discarded);
                        manager.enemy.discarded.Remove(targetCard);
                        manager.enemyField.Add(targetCard);
                        manager.enemyAvailableFieldSlots--;
                        //render card
                        manager.enemy.RenderCard(targetCard);
                        targetCard.summonState = SummonState.BattleReady;
                        targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                    case 5:
                        targetCard = GetHighestDefense(manager.enemy.discarded);
                        manager.enemy.discarded.Remove(targetCard);
                        manager.enemyField.Add(targetCard);
                        manager.enemyAvailableFieldSlots--;
                        //render card
                        manager.enemy.RenderCard(targetCard);
                        targetCard.summonState = SummonState.BattleReady;
                        targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                    case 6:
                        targetCard = GetHighestDefense(manager.enemy.discarded);
                        manager.enemy.discarded.Remove(targetCard);
                        manager.enemyField.Add(targetCard);
                        manager.enemyAvailableFieldSlots--;
                        //render card
                        manager.enemy.RenderCard(targetCard);
                        targetCard.summonState = SummonState.BattleReady;
                        targetCard.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
                        Debug.Log("Enemy revived " + targetCard.name + "!");
                        break;
                }
                break;
            case "Player":

                break;
        }
    }



    Card GetHighestAttack(List<Card> field) {
        int tempAttack = int.MinValue;
        Card targetCard = null;
        foreach (Card c in field) {
            if (c.attack > tempAttack) {
                tempAttack = c.attack;
                targetCard = c;
            }
        }
        return targetCard;
    }

    Card GetLowestAttack(List<Card> field)
    {
        int tempAttack = int.MaxValue;
        Card targetCard = null;
        foreach (Card c in field)
        {
            if (c.attack < tempAttack)
            {
                tempAttack = c.attack;
                targetCard = c;
            }
        }
        return targetCard;
    }

    Card GetHighestDefense(List<Card> field)
    {
        int tempDefense = int.MinValue;
        Card targetCard = null;
        foreach (Card c in field)
        {
            if (c.defense > tempDefense)
            {
                tempDefense = c.defense;
                targetCard = c;
            }
        }
        return targetCard;
    }


    Card GetLowestDefense(List<Card> field)
    {
        int tempDefense = int.MaxValue;
        Card targetCard = null;
        foreach (Card c in field)
        {
            if (c.defense < tempDefense)
            {
                tempDefense = c.defense;
                targetCard = c;
            }
        }
        return targetCard;
    }

    Card GetRandomCardOnField(List<Card> field) {
        int index = Random.Range(0, field.Count);
        return field[index];
    }
}
