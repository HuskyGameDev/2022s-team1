using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public float GlobalVolume;
    public AudioMixer mixer;

    GameObject _sliderObj;

    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
        mixer.SetFloat("AudioVolume", Mathf.Log10(GlobalVolume)*20);
    }

    void Awake()
    {
        LoadSettings();
        mixer.SetFloat("AudioVolume", Mathf.Log10(GlobalVolume)*20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveVolumeSettings(){
        SaveSystem.SaveGameSettings(GlobalVolume);
    }

    public void LoadSettings(){
        
        SaveGameSettings data = SaveSystem.LoadGameSettings();

        if (data != null){
            this.GlobalVolume = data.gameVolume;
        } else {
            this.GlobalVolume = 1.0f;
        }
        
        _sliderObj = GameObject.Find("Slider");
        if (_sliderObj != null){
            _sliderObj.GetComponent<Slider>().value = this.GlobalVolume;
        }

    }


}
