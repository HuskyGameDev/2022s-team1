using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameSettings
{
    //public Deck deck; Decided it was best to just keep the dex in mind.

    public float gameVolume;

    public SaveGameSettings(float Vol) {
        gameVolume = Vol;
        Debug.Log(gameVolume);
    }

}
