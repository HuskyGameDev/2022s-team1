using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This is the Scriptable object template for creating cards for CAZ.
 * This scrpit allows us to create "Card" objects in the Unity editor for storing their information.
 * Note:
 *      1) Any card can draw from this information during runtime - which is how we will be dealing with cards during combat/ in the deck builder.
 *      2) This script is used in conjunction with the "DisplayCard" script in order to updade card's information fields during runtime.
 **/

public enum Types { Creature, Effect, Boss }

public enum SummonState { SummonSick, BattleReady }

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public GameObject prefab;

    public GameObject cardObject;

    public int fieldIndex;
    
    public new string name;     // Name of the card

    public Types type;

    public SummonState summonSate;

    public string description;  // Description of the card

    public Sprite art;          // Artwork for the card

    public int attack;          // Attack score of the card

    public int defense;         // Defense score of the card
}
