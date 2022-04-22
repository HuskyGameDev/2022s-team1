using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour {

    public AudioMixer mixer;

    public void SetVolLevel(float _sliderValue) {

        //Let's make sure we save to the gamedata as well.
        GameObject.Find("Settings Manager").GetComponent<SettingsManager>().GlobalVolume = _sliderValue;
        
        mixer.SetFloat("AudioVolume", Mathf.Log10(_sliderValue)*20);

    }
    
    // Start is called before the first frame update
    void Start()
    {
        //GameObject _audManager = GameObject.Find("AudioManager");
        //GetComponent<Slider>().value = _audManager.GetComponent<AudioManager>().GlobalVolume;
    }

    void Awake(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
