using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_2_Standard_Maxmimize : AI_Standard
{
    public override int EvaluateFieldScore(Card card)
    {
        int FS = 0;
        FS += health;
        FS -= player.health;
        for (int i = 0; i < manager.enemyField.Count; i++)
        {
            FS += 5;
            FS += manager.enemyField[i].attack;
            FS += manager.enemyField[i].defense;
        }
        for (int i = 0; i < manager.playerField.Count; i++)
        {

            FS -= 5;
            FS -= manager.playerField[i].attack;
            FS -= manager.playerField[i].defense;
        }

        FS += 5;
        FS += card.attack;
        FS += card.defense;

        return FS;
    }

    public override IEnumerator Attacks()
    {
        Card target = null;
        int tempAttackScore = 0;
        int attackScore = int.MinValue;
        for (int i = 0; i < manager.enemyField.Count; i++)
        { // all creatures can attack
            yield return new WaitForSeconds(1f);
            if (manager.playerField.Count > 0 && manager.enemyField[i].summonState == SummonState.BattleReady)
            {
                manager.enemyField[i].cardObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(true); // set select overlay
                yield return new WaitForSeconds(1f);
                for (int j = 0; j < manager.playerField.Count; j++)
                { // check each creature in player field
                    Debug.Log("Checking if " + manager.enemyField[i].name + " can destroy " + manager.playerField[j].name);
                    if (manager.enemyField[i].attack >= manager.playerField[j].defense) // if creature can destroy a player's creature
                    {
                        Debug.Log(manager.enemyField[i].name + " can destroy " + manager.playerField[j].name + " Checking if this is a good attack");
                        tempAttackScore = manager.enemyField[i].attack - manager.playerField[j].defense;
                        if (tempAttackScore > attackScore)
                        { // check to see if it is optimal
                            attackScore = tempAttackScore;
                            if (target != null)
                            {
                                target.cardObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(false); // set select overlay
                            }
                            target = manager.playerField[j]; // select target
                            Debug.Log(manager.enemyField[i].name + " has targeted " + manager.playerField[j].name);
                            yield return new WaitForSeconds(1f);
                            manager.playerField[j].cardObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(true); // set select overlay
                        }
                        else
                        {
                            Debug.Log(manager.enemyField[i].name + " does not target " + manager.playerField[j].name);
                            yield return new WaitForSeconds(1f);
                            manager.playerField[j].cardObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(false); // set select overlay
                        }
                    }
                }
                if (target != null)
                {
                    Debug.Log(manager.enemyField[i].name + " is attacking " + target.name);
                    yield return new WaitForSeconds(1f);
                    AttackAndDestroy(manager.enemyField[i], target); // destroy target card
                    target = null;
                    attackScore = int.MinValue; // reset attackScore for next card
                    manager.enemyField[i].cardObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false); // set select overlay
                    continue;
                }
                else if (target == null)
                {
                    Debug.Log(manager.enemyField[i].name + " cannot attack any player cards this turn");
                    yield return new WaitForSeconds(1f);
                    attackScore = int.MinValue; // reset attackScore for next card
                    manager.enemyField[i].cardObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false); // set select overlay
                    continue;
                }
                //attackScore = int.MaxValue; // reset attackScore for next card
            }
            else if (manager.enemyField[i].summonState == SummonState.BattleReady)
            {
                Debug.Log(manager.enemyField[i].name + " attacks directly!");
                DamagePlayer(manager.enemyField[i].attack);
                yield return new WaitForSeconds(1f);
                manager.enemyField[i].cardObject.GetComponent<CardDisplay>().playerSelectOverlay.SetActive(false); // set select overlay
            }
            else
            {
                Debug.Log(manager.enemyField[i].name + " has summoning sickeness and cannot attack this round.");
                //c.summonState = SummonState.BattleReady;
                //summonSickCreatures.Add(manager.enemyField[i]);
                //manager.enemyField[i].summonState = SummonState.BattleReady;
                //manager.enemyField[i].summonState = SummonState.BattleReady;
            }
        }
        DestroyMarkedCards();
    }

    public override IEnumerator Effects()
    {
        //check if effect cards are in hand
        string effectName;
        var handclone = new List<Card>(hand);
        foreach (Card c in handclone)
        {
            if (c.type == Types.Effect)
            {
                effectName = c.name;
                switch (effectName)
                {
                    case "Healing Potion":
                        //check if healing potion is valuable
                        if (health < maxHealth - 5)
                        {
                            yield return new WaitUntil(() => manager.activeEffect == ActiveEffect.NONE);
                            yield return new WaitForSeconds(1f);
                            hand.Remove(c);
                            RenderEffectCard(c);
                            manager.effects.HealingPotion("Enemy");
                            yield return new WaitForSeconds(2f);
                            EraseCard(c);
                        }
                        break;
                    case "Sleight of Hand":
                        //check if sleight of hand is valuable
                        if (deck.Count > 6 && hand.Count < 3)
                        {
                            yield return new WaitUntil(() => manager.activeEffect == ActiveEffect.NONE);
                            yield return new WaitForSeconds(1f);
                            hand.Remove(c);
                            RenderEffectCard(c);
                            manager.effects.SleightOfHand("Enemy");
                            yield return new WaitForSeconds(2f);
                            EraseCard(c);
                        }
                        break;
                    case "Sacrifice":
                        //check if sacrifice is valuable
                        if (manager.enemyField.Count == 3 && manager.playerField.Count == 3)
                        {
                            yield return new WaitUntil(() => manager.activeEffect == ActiveEffect.NONE);
                            yield return new WaitForSeconds(1f);
                            hand.Remove(c);
                            RenderEffectCard(c);
                            yield return StartCoroutine(manager.effects.Sacrifice("Enemy", 2));
                            yield return new WaitForSeconds(2f);
                            EraseCard(c);
                        }
                        break;
                    case "Shadow Strike":
                        if (manager.playerField.Count >= 1)
                        {
                            yield return new WaitUntil(() => manager.activeEffect == ActiveEffect.NONE);
                            yield return new WaitForSeconds(1f);
                            hand.Remove(c);
                            RenderEffectCard(c);
                            yield return StartCoroutine(manager.effects.ShadowStrike("Enemy", 2));
                            yield return new WaitForSeconds(2f);
                            EraseCard(c);
                        }
                        break;
                    //check if shadow strike is valuable
                    case "Aggression":
                        //check if aggression is valuable
                        bool nonAggroCreatureOnField = NonAggroOnField();
                        float aggroRand = Random.Range(1, 4);
                        if (aggroRand == 3 && nonAggroCreatureOnField)
                        {
                            yield return new WaitUntil(() => manager.activeEffect == ActiveEffect.NONE);
                            yield return new WaitForSeconds(1f);
                            hand.Remove(c);
                            RenderEffectCard(c);
                            yield return StartCoroutine(manager.effects.Aggression("Enemy", 2));
                            yield return new WaitForSeconds(2f);
                            EraseCard(c);
                        }
                        break;
                    case "Revive":
                        //check if revive is valuable
                        if (discarded.Count > 0 && hand.Count < 3)
                        {
                            yield return new WaitUntil(() => manager.activeEffect == ActiveEffect.NONE);
                            yield return new WaitForSeconds(1f);
                            hand.Remove(c);
                            RenderEffectCard(c);
                            manager.effects.Revive("Enemy", 2);
                            yield return new WaitForSeconds(2f);
                            EraseCard(c);
                        }
                        break;
                    case "Shield":
                        //check if shield is valuable
                        bool nonShieldedCreatureOnField = NonShieldedOnField();
                        float shieldRand = Random.Range(1, 4);
                        if (shieldRand == 3 && nonShieldedCreatureOnField)
                        {
                            yield return new WaitUntil(() => manager.activeEffect == ActiveEffect.NONE);
                            yield return new WaitForSeconds(1f);
                            hand.Remove(c);
                            RenderEffectCard(c);
                            yield return StartCoroutine(manager.effects.Shield("Enemy", 2));
                            yield return new WaitForSeconds(2f);
                            EraseCard(c);
                        }
                        break;
                    default:
                        //effect not found
                        Debug.Log("ERROR - Effect card not found");
                        break;
                }
            }
        }
        //check if effect cards are valuable
        //play effect cards
    }
}
