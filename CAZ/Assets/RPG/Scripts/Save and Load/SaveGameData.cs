using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameData
{
    //public Deck deck; Decided it was best to just keep the dex in mind.
    public DexData dex;
    public GameManager.Level level;
    public int deckMax;
    public int battleHP;
    public bool discovered_forest;
    public bool discovered_cave;
    public bool discovered_castle;

    public float gameVolume;

    public SaveGameData(GameManager gm) {
        dex = new DexData(gm);
        //deck = gm.deck;
        level = gm.currentLevel;
        deckMax = gm.deckMax;
        battleHP = gm.battleHp;
        discovered_forest = gm.discovered_forest;
        discovered_cave = gm.discovered_cave;
        discovered_castle = gm.discovered_castle;
    }


    [System.Serializable]
    public class DexData 
    {

        List<bool> discoveredCards; //Basically, because of how the dex is set up, we only need to go right in order. 
        //So from the top, see which is discovered and note it as such.

        public DexData(GameManager gm){

            discoveredCards = new List<bool>(); //I'm shit actually

            foreach (CardDex.CardEntry c in gm.dex.cardDex){
                Debug.Log(c.isDiscovered);
                discoveredCards.Add(c.isDiscovered);
            }

        }

        public void GetDiscoveredCards(CardDex dex){

            for (int i = 0; i < dex.cardDex.Count; i++){
                dex.cardDex[i].isDiscovered = discoveredCards[i];
            }

        }

    }

}
