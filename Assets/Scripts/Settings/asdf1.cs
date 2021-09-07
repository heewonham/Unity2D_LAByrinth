using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class asdf1 : MonoBehaviour
{
    public Slider BackVolume;
    public AudioSource audio;

    private float backVol = 1f;

    private void Start()
    {
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        BackVolume.value = backVol;
        audio.volume = BackVolume.value;
    }
    
    void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        audio.volume = BackVolume.value;

        backVol = BackVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }

   

}
