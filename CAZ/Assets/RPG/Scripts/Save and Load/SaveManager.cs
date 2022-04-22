using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public bool loadGame;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SaveGame() {

        SaveSystem.SaveGameData(GameManager.instance);
    }

    public void LoadGame() {
        SaveGameData data = SaveSystem.LoadGameData();

        data.dex.GetDiscoveredCards(GameManager.instance.dex);
        //GameManager.instance.deck = data.deck;
        GameManager.instance.currentLevel = data.level;
        GameManager.instance.deckMax = data.deckMax;
        GameManager.instance.battleHp = data.battleHP;
        GameManager.instance.discovered_forest = data.discovered_forest;
        GameManager.instance.discovered_cave = data.discovered_cave;
        GameManager.instance.discovered_castle = data.discovered_castle;
    }
}
