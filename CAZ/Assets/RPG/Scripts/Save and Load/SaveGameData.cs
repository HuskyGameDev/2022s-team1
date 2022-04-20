using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameData
{
    public CardDex dex;
    public Deck deck;
    public GameManager.Level level;
    public int deckMax;
    public int battleHP;
    public bool discovered_forest;
    public bool discovered_cave;
    public bool discovered_castle;


    public SaveGameData(GameManager gm) {
        dex = gm.dex;
        deck = gm.deck;
        level = gm.currentLevel;
        deckMax = gm.deckMax;
        battleHP = gm.battleHp;
        discovered_forest = gm.discovered_forest;
        discovered_cave = gm.discovered_cave;
        discovered_castle = gm.discovered_castle;
    }
}
