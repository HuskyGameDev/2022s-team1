using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    public GameObject backwardsOverworldController;
    //public GameObject bossTrigger;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.bossBattle = false;
        Debug.Log("Castle Loaded");
        AudioManager.instance.Stop("Main_Theme");
        AudioManager.instance.Play("Castle_EXT_Overworld");
        AudioManager.instance.overworldSong = "Castle_EXT_Overworld";
        if (!GameManager.instance.discovered_castle) {
            GameManager.instance.deckMax = 30;
            GameManager.instance.battleHp = 40;
        }

        PlayerController player = GameManager.instance.player.GetComponent<PlayerController>();
        if (!player.battlePrimed)
            player.battleCooldownTimer = player.battleCooldownTime;

    }

}
