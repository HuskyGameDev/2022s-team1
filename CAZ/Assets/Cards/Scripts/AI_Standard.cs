using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Standard : MonoBehaviour
{

    /**
     * Enemy Turn Algorithm:
     * 
     * 1. Draw Cards
     * 2. Determine best move
     *      2.1 how many field slots are open?
     *      2.2 how many creatures in hand?
     *      2.3 IF # of creatures in hand less than open field slots
     *          THEN play all creatures on the field
     *      2.4 IF # of creatures in hand greater than open field slots
     *          THEN calculate FS for next creature than maximizes FS and play creature on field, repeat until field slots are full
     * 4. Perform Field Tasks (attacking, effect cards, etc.)
     * 5. End Turn
     * 
     */

    public int health;
    public int maxHealth;
    public List<Card> deck;
    public List<Card> hand;
    public List<Card> discarded;
    public Card bestNextCard;
    public List<Card> markedCards;
    public List<Card> summonSickCreatures;

    public EncounterManager manager;
    public PlayerUnit player;

    private void Awake()
    {
        maxHealth = health;
    }

    public IEnumerator PlayTurn() {
        PrintDeck();
        yield return new WaitForSeconds(2f);
        StartCoroutine(DrawCards()); // Draw Cards
        PrintHand();
        yield return new WaitForSeconds(2f);
        PrintDeck();
        yield return new WaitForSeconds(2f);
        StartCoroutine(DetermineMove());
        yield return new WaitForSeconds(2f);
        PrintField();
        player.PrintField();
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(PerformFieldTasks());
        yield return new WaitForSeconds(2f);
        PrintField();
        player.PrintField();
        Debug.Log("Enemy turn ended!");
        StartCoroutine(manager.PlayerTurn());
    }
    public IEnumerator DrawCards()
    {
        Debug.Log("Drawing Cards...");
        //draw cards from deck, add to the hand until hand is full (max 3)
        while(hand.Count < 3) {
            Card drawnCard = Instantiate(deck[deck.Count - 1]);
            hand.Add(drawnCard);
            manager.handNum.text = hand.Count.ToString(); // relay visual info on hands in card;
            Debug.Log("Enemy Draws " + drawnCard.name);
            deck.RemoveAt(deck.Count - 1);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator DetermineMove() {
        int tempFS = 0; // store most recently calculated Field Score
        int maxFS = int.MinValue; // keep track of highest calculated Field Score

        bool creatureInHand = CreatureInHand();

        while(manager.enemyAvailableFieldSlots > 0 && creatureInHand) { // while there are open field slots

            foreach (Card c in hand) { // Evaluate potential Field Score for each creature in the hand
                if (c.type == Types.Creature) {
                    Debug.Log("Evaluating FS for " + c.name + "...");
                    tempFS = EvaluateFieldScore(c);
                    Debug.Log("FS for " + c.name + " = " + tempFS);
                    if (tempFS > maxFS) {
                        maxFS = tempFS;
                        bestNextCard = c; // keep track of best next card based on max Field Score
                        Debug.Log("Best next card is: " + c.name);
                    }
                }
            }
            yield return new WaitForSeconds(2f);
            PlayCard(bestNextCard);
            creatureInHand = CreatureInHand();
            maxFS = int.MinValue; // reset maxFS
        }
    }

    bool CreatureInHand() {
        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].type == Types.Creature) {
                return true;
            }
        }
        return false;
    }

    void PlayCard(Card card) {
        Debug.Log("Playing " + card.name + " onto the field.");
        hand.Remove(card);
        manager.handNum.text = hand.Count.ToString(); // relay visual info on hands in card;
        manager.enemyField.Add(card);
        manager.enemyAvailableFieldSlots--;
        //card.summonState = SummonState.SummoningSickness;
        RenderCard(card); // relay visual info on cards on field
    }

    public void RenderCard(Card card) {
        /*
        GameObject newCard = Instantiate(manager.cardPrefab, manager.enemyFieldSlots[manager.enemyAvailableFieldSlots]);
        newCard.GetComponent<CardDisplay>().card = card;
        newCard.GetComponent<CardDisplay>().Display();
        */
        int freeIndex = 0;
        for (int i = 0; i < manager.enemyFieldSlotAvailability.Count; i++) {
            if (manager.enemyFieldSlotAvailability[i] == 0)
                freeIndex = i;
        }
        
        GameObject newCard = Instantiate(card.prefab, manager.enemyFieldSlots[freeIndex]);
        newCard.transform.localScale = new Vector3(51, 40, 1);
        newCard.GetComponent<Draggable>().owner = Draggable.Owner.ENEMY;
        newCard.GetComponent<Draggable>().placed = true;
        manager.enemyFieldSlotAvailability[freeIndex] = 1;
        card.fieldIndex = freeIndex;
        card.cardObject = newCard;
        card.summonState = SummonState.SummonSick;
        card.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(true);
        card.cardObject.GetComponent<CardDisplay>().card = card;
        card.cardObject.GetComponent<CardDisplay>().Display();

        //newCard.GetComponent<CardDisplay>().card = card;
        //newCard.GetComponent<CardDisplay>().Display();
    }

    public void RenderEffectCard(Card card) {
        GameObject newCard = Instantiate(card.prefab, manager.enemyEffectSlot);
        newCard.transform.localScale = new Vector3(51, 40, 1);
        card.cardObject = newCard;
        card.cardObject.GetComponent<CardDisplay>().card = card;
        card.cardObject.GetComponent<CardDisplay>().Display();
    }

    /**
     * FOR VISUALS OF CARD GAME:
     * NEED TO CHANGE: implementation of enemyField - change to List<GameObject> and reference Card2.0's card variable
     * This will allow you to instantiate and destroy the correct card game object while still having access to card information.
     * Might have to change other List<Card>'s to List<GameObject>'s as well.
     * Big OOF but needs to be done
     * 
     * OR change card scriptable object to include card 2.0 prefab and draw from itself ???
     */
    public void EraseCard(Card card) {
        Destroy(card.cardObject);
    }


    public virtual int EvaluateFieldScore(Card card) {
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

    IEnumerator PerformFieldTasks() {
        Debug.Log("Enemy Starts Effect Round!!!");
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Effects());
        Debug.Log("Enemy Starts Attack Round!!!");
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Attacks());
        Debug.Log("Enemy Ends Attack Round!!!");
        yield return new WaitForSeconds(2f);
        CureSummonSickness();
    }

    public virtual IEnumerator Effects() {
        //check if effect cards are in hand
        string effectName;
        var handclone = new List<Card>(hand);
        foreach (Card c in handclone) {
            if (c.type == Types.Effect) {
                effectName = c.name;
                switch (effectName) {
                    case "Healing Potion":
                        //check if healing potion is valuable
                        if (health < maxHealth - 5)
                        {
                            hand.Remove(c);
                            manager.handNum.text = hand.Count.ToString(); // relay visual info on hands in card;
                            RenderEffectCard(c);
                            manager.effects.HealingPotion("Enemy");
                            yield return new WaitForSeconds(3f);
                            EraseCard(c);
                        }
                        break;
                    case "Sleight of Hand":
                        //check if sleight of hand is valuable
                        if (deck.Count > 6 && hand.Count < 3)
                        {
                            hand.Remove(c);
                            manager.handNum.text = hand.Count.ToString(); // relay visual info on hands in card;
                            RenderEffectCard(c);
                            manager.effects.SleightOfHand("Enemy");
                            yield return new WaitForSeconds(3f);
                            EraseCard(c);
                        }
                        break;
                    case "Sacrifice":
                        //check if sacrifice is valuable
                        if (manager.enemyField.Count == 3 && manager.playerField.Count == 3)
                        {
                            hand.Remove(c);
                            manager.handNum.text = hand.Count.ToString(); // relay visual info on hands in card;
                            RenderEffectCard(c);
                            manager.effects.Sacrifice("Enemy", 1);
                            yield return new WaitForSeconds(3f);
                            EraseCard(c);
                        }
                        break;
                    case "Shadow Strike":
                        if (manager.playerField.Count >= 1)
                        {
                            hand.Remove(c);
                            manager.handNum.text = hand.Count.ToString(); // relay visual info on hands in card;
                            RenderEffectCard(c);
                            manager.effects.ShadowStrike("Enemy", 1);
                            yield return new WaitForSeconds(3f);
                            EraseCard(c);
                        }
                        break;
                    //check if shadow strike is valuable
                    case "Aggression":
                        //check if aggression is valuable
                        float aggroRand = Random.Range(1, 4);
                        if (aggroRand == 3)
                        {
                            hand.Remove(c);
                            manager.handNum.text = hand.Count.ToString(); // relay visual info on hands in card;
                            RenderEffectCard(c);
                            manager.effects.Aggression("Enemy", 1);
                            yield return new WaitForSeconds(3f);
                            EraseCard(c);
                        }
                        break;
                    case "Revive":
                        //check if revive is valuable
                        if (discarded.Count > 0 && manager.enemyAvailableFieldSlots > 0)
                        {
                            hand.Remove(c);
                            manager.handNum.text = hand.Count.ToString(); // relay visual info on hands in card;
                            RenderEffectCard(c);
                            manager.effects.Revive("Enemy", 1);
                            yield return new WaitForSeconds(3f);
                            EraseCard(c);
                        }
                        break;
                    case "Shield":
                        //check if shield is valuable
                        float shieldRand = Random.Range(1, 4);
                        if (shieldRand == 3)
                        {
                            hand.Remove(c);
                            manager.handNum.text = hand.Count.ToString(); // relay visual info on hands in card;
                            RenderEffectCard(c);
                            manager.effects.Shield("Enemy", 1);
                            yield return new WaitForSeconds(3f);
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

    public virtual IEnumerator Attacks() {
        Card target = null;
        int tempAttackScore = 0;
        int attackScore = int.MaxValue;
        for(int i = 0; i < manager.enemyField.Count; i++) { // all creatures can attack
            yield return new WaitForSeconds(3f);
            if (manager.playerField.Count > 0 && manager.enemyField[i].summonState == SummonState.BattleReady)
            {
                for(int j = 0; j < manager.playerField.Count; j++)
                { // check each creature in player field
                    Debug.Log("Checking if " + manager.enemyField[i].name + " can destroy " + manager.playerField[j].name);
                    if (manager.enemyField[i].attack >= manager.playerField[j].defense) // if creature can destroy a player's creature
                    {
                        Debug.Log(manager.enemyField[i].name + " can destroy " + manager.playerField[j].name + " Checking if this is a good attack");
                        tempAttackScore = manager.enemyField[i].attack - manager.playerField[j].defense;
                        if (tempAttackScore < attackScore)
                        { // check to see if it is optimal
                            attackScore = tempAttackScore;
                            target = manager.playerField[j]; // select target
                            Debug.Log(manager.enemyField[i].name + " has targeted " + manager.playerField[j].name);
                        }
                        else
                        {
                            Debug.Log(manager.enemyField[i].name + " does not target " + manager.playerField[j].name);
                        }
                    }
                }
                if (target != null)
                {
                    Debug.Log(manager.enemyField[i].name + " is attacking " + target.name);
                    AttackAndDestroy(manager.enemyField[i], target); // destroy target card
                    target = null;
                    attackScore = int.MaxValue; // reset attackScore for next card
                    continue;
                }
                else if (target == null)
                {
                    Debug.Log(manager.enemyField[i].name + " cannot attack any player cards this turn");
                    attackScore = int.MaxValue; // reset attackScore for next card
                    continue;
                }
                //attackScore = int.MaxValue; // reset attackScore for next card
            }
            else if (manager.enemyField[i].summonState == SummonState.BattleReady)
            {
                Debug.Log(manager.enemyField[i].name + " attacks directly!");
                DamagePlayer(manager.enemyField[i].attack);
            }
            else {
                Debug.Log(manager.enemyField[i].name + " has summoning sickeness and cannot attack this round.");
                //c.summonState = SummonState.BattleReady;
                //summonSickCreatures.Add(manager.enemyField[i]);
                //manager.enemyField[i].summonState = SummonState.BattleReady;

                //manager.enemyField[i].summonState = SummonState.BattleReady;
                //manager.enemyField[i].cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
                
            }
        }
        DestroyMarkedCards();
    }

    public void AttackAndDestroy(Card aggressor, Card receiver) {

        if (aggressor.attack > receiver.defense)
        { // double check attack/defense values
            manager.playerField.Remove(receiver); // remove from field
            Debug.Log(aggressor.name + " destroys " + receiver.name);
            manager.playerFieldSlots[receiver.fieldIndex].GetComponent<DropZone>().taken = false;
            EraseCard(receiver);

            manager.player.discarded.Add(receiver); // add to discard pile
            manager.playerDiscardController.addCardToContent(receiver);
            Debug.Log(receiver.name + " is sent to the discard pile");

            int damageRemainder = aggressor.attack - receiver.defense;
            //DamagePlayer(damageRemainder); // Uncomment if destroying creature deals damage
        }
        else if (aggressor.attack == receiver.defense) {
            // Destroy defending card
            manager.playerField.Remove(receiver); // remove from field
            Debug.Log(aggressor.name + " destroys " + receiver.name);
            manager.playerFieldSlots[receiver.fieldIndex].GetComponent<DropZone>().taken = false;
            EraseCard(receiver);

            manager.player.discarded.Add(receiver); // add to discard pile
            manager.playerDiscardController.addCardToContent(receiver);
            Debug.Log(receiver.name + " is sent to the discard pile");

            // Mark attacking card for destruction after attack phase
            markedCards.Add(aggressor);
            aggressor.cardObject.GetComponent<CardDisplay>().attackSelectOverlay.SetActive(true);
            Debug.Log(aggressor.name + " was wounded in the attack!");
        }
        
    }

    public void DamagePlayer(int damage) {
        manager.player.TakeDamage(damage);
    }

    public void DestroyMarkedCards() {

        for (int i =0; i < markedCards.Count; i++) {
            manager.enemyField.Remove(markedCards[i]);
            EraseCard(markedCards[i]);
            manager.enemyAvailableFieldSlots++;
            manager.enemyFieldSlotAvailability[markedCards[i].fieldIndex] = 0;
            Debug.Log(markedCards[i].name + " fell to its wounds and was destroyed!");
            discarded.Add(markedCards[i]);
            manager.enemyDiscardController.addCardToContent(markedCards[i]);
            Debug.Log(markedCards[i].name + " was sent to the discard pile");
        }
        markedCards.Clear();
    }

    
    public void CureSummonSickness() {
        foreach (Card c in manager.enemyField) {
            Debug.Log(c.name + " is no longer summoning sick and is ready to fight!");
            c.summonState = SummonState.BattleReady;
            c.cardObject.GetComponent<CardDisplay>().summonSickOverlay.SetActive(false);
        }
        //summonSickCreatures.Clear();
    }
    

    public void TakeDamage(int damage) {
        health -= damage;
        manager.enemyHPText.text = health.ToString();
        Debug.Log("Enemy takes " + damage + " points of Damage! | Enemy's HP: " + health);
    }

    public void PrintHand() {
        Debug.Log("Cards in hand: ");
        for (int i = 0; i < hand.Count; i++) {
            Debug.Log("\t" + hand[i].name + " atk:" + hand[i].attack + " def:" + hand[i].defense);
        }
    }

    public void PrintDeck()
    {
        Debug.Log("Cards in Deck: ");
        for (int i = 0; i < deck.Count; i++)
        {
            Debug.Log("\t" + deck[i].name);
        }
    }

    public void PrintField()
    {
        Debug.Log("Cards in field: ");
        for (int i = 0; i < manager.enemyField.Count; i++)
        {
            Debug.Log("\t" + manager.enemyField[i].name);
        }
    }
}
