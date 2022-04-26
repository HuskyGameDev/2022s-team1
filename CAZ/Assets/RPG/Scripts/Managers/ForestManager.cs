using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestManager : MonoBehaviour
{
    public GameObject backwardsOverworldController;
    //public GameObject bossTrigger;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.bossBattle = false;

        Debug.Log("Forest Loaded");
        AudioManager.instance.Stop("Main_Theme");
        AudioManager.instance.Play("Forest_Overworld");
        AudioManager.instance.overworldSong = "Forest_Overworld";
        if (!GameManager.instance.discovered_forest) {
            //GameManager.instance.discovered_forest = true;
            GameManager.instance.deckMax = 20;
            GameManager.instance.battleHp = 25;
        }

        PlayerController player = GameManager.instance.player.GetComponent<PlayerController>();
        if (!player.battlePrimed)
            player.battleCooldownTimer = player.battleCooldownTime;

    }

}
