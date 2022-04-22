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
        AudioManager.instance.Play("Main_Theme");
        AudioManager.instance.overworldSong = "Main_Theme";
        if (GameManager.instance.discovered_forest) {
            bossTrigger.SetActive(false);
        }

    }

}
