using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoBehaviour
{

    public GameObject bossTrigger;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Stop(AudioManager.instance.overworldSong);
        GameManager.instance.bossBattle = false;
        GameObject _gameObj = GameObject.Find("AudioManager");
        _gameObj.GetComponent<AudioManager>().Stop("Menu_Theme");
        AudioManager.instance.Play("Main_Theme");
        AudioManager.instance.overworldSong = "Main_Theme";
        if (GameManager.instance.discovered_forest) {
            bossTrigger.SetActive(false);
        }

        PlayerController player = GameManager.instance.player.GetComponent<PlayerController>();
        if (!player.battlePrimed)
            player.battleCooldownTimer = player.battleCooldownTime;
    }

}
