using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveManager : MonoBehaviour
{
    public GameObject backwardsOverworldController;
    //public GameObject bossTrigger;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.bossBattle = false;
        Debug.Log("Cave Loaded");
        AudioManager.instance.Stop("Main_Theme");
        AudioManager.instance.Play("Cave_Overworld");
        AudioManager.instance.overworldSong = "Cave_Overworld";
        if (!GameManager.instance.discovered_cave) {
            //GameManager.instance.discovered_cave = true;
            GameManager.instance.deckMax = 30;
            GameManager.instance.battleHp = 30;
        }

        PlayerController player = GameManager.instance.player.GetComponent<PlayerController>();
        if (!player.battlePrimed)
            player.battleCooldownTimer = player.battleCooldownTime;

    }

}
